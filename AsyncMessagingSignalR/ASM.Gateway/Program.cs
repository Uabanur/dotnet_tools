using System.Text;
using ASM.Gateway.Hubs;
using ASM.Gateway.Workers;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.AddRabbitMQ("RabbitMQConnection", configureConnectionFactory: cf => {
    using var con = cf.CreateConnection();
    using var channel = con.CreateModel();

    channel.QueueDeclare(
        queue: "message",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );
    channel.QueueDeclare(
        queue: "response",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );
});

builder.Services.AddHostedService<MessageResponseWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<MessageHub>("/messages");

app.MapGet("/rabbitmq", (IConnection con) => {
    var channel = con.CreateModel();
    channel.BasicPublish(
        exchange: "",
        routingKey: "message",
        basicProperties: null,
        body: Encoding.UTF8.GetBytes("Message")
    );
});

app.Run();
