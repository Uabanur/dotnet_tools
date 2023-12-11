using GraphQL;
using Library.BookStore;
using Library.Graph;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddSingleton<IBookDatabase, BookDatabase>();
builder.Services.AddSingleton<BookQuery>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddGraphQL(builder => builder
    .AddGraphTypes()
    .AddSystemTextJson()
    .AddSchema<BookSchema>());

var app = builder.Build();
app.UseGraphQL<BookSchema>()
    .UseGraphQLPlayground();

app.Run();
