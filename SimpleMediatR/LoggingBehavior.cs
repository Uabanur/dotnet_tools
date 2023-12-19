using MediatR;
using System.Diagnostics;
using System.Text.Json;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
  private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

  public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
  {
    var start = Stopwatch.StartNew();

    _logger.LogInformation("Handling request {Type}, content: {Request}", 
        typeof(TRequest), JsonSerializer.Serialize(request));

    var response = await next();

    _logger.LogInformation("Request handled {Type}, duration: {Duration} ms", 
        typeof(TRequest), start.Elapsed.TotalMilliseconds);

    return response;
  }
}

