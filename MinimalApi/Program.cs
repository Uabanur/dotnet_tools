var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new Todos();
app.MapGet("/", () => "Hello");

var todoitems = app.MapGroup("/todoitems");
todoitems.MapGet("/", () => todos.List());
todoitems.MapGet("/complete", () => todos.Complete());
todoitems.MapGet("/{id}", (Guid id) => todos.Get(id));
todoitems.MapPost("/", (Todo entry) => todos.Add(entry));
todoitems.MapPut("/{id}", (Guid id, Todo update) => todos.Update(id, update));
todoitems.MapDelete("/{id}", (Guid id) => todos.Delete(id));

app.Run();
