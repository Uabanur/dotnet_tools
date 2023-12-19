using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(config => config.AddConsole());

builder.Services.AddMediatR(cfg => 
{
  cfg.RegisterServicesFromAssemblyContaining<Program>();
  cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

});

var app = builder.Build();

var mediatr = app.Services.GetRequiredService<IMediator>();
app.MapGet("/", () => "Hello World!");
app.MapGet("/mediatr/{name}", async (string name) => 
    await mediatr.Send(new MessageRequest(name)));

app.Run();
