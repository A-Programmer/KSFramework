using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.Messaging.Behaviors;

/// <summary>
/// Logs the request before and after it is handled.
/// </summary>
/// <typeparam name="TRequest">The type of request.</typeparam>
/// <typeparam name="TResponse">The type of response.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("[Logging] Handling {Request}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("[Logging] Handled {Request}", typeof(TRequest).Name);
        return response;
    }
}