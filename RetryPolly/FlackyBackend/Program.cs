var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/info", () => 
{
	if (Random.Shared.Next(2) == 0)
		throw new Exception("Crash");

	return "Important message";
});

app.Run();
