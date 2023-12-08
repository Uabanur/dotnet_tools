using Microsoft.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using RetryPolly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient(InfoService.ClientName, 
    client => 
    {
        client.BaseAddress = new Uri("http://dep:5000");
    })
    .AddTransientHttpErrorPolicy(policyBuilder => 
            policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

builder.Services.AddScoped<IInfoService, InfoService>();

var app = builder.Build();
app.MapGet("/", async (IInfoService infoService) => await infoService.GetInfo());
app.MapGet("/check/", () => "Ok");

app.Run();
