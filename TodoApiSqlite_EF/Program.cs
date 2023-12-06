using TodoApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<TodoContext>();

var app = builder.Build();
var logger = app.Logger;

app.Services
    .GetRequiredService<TodoContext>()
    .Database
    .Migrate();

app.MapGet("/", (TodoContext db) => 
{
    logger.LogInformation($"Inserting new todo");
    db.Add(new Todo{ Name = "Test todo" });
    db.SaveChanges();

    logger.LogInformation("Querying for a todo");
    var todo = db.Todos
        .OrderBy(b => b.Id)
        .First();
    logger.LogInformation("Got: {Id} {Name} {Completed}", todo.Id, todo.Name, todo.Completed);


    logger.LogInformation("Removing todo");
    db.Remove(todo);
    db.SaveChanges();

    return "Ok";
});

app.Run();
