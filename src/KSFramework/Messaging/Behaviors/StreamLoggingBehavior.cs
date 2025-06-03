using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.Messaging.Behaviors;

/// <summary>
/// A stream pipeline behavior that logs before and after streaming a request.
/// </summary>
/// <typeparam name="TRequest">The stream request type.</typeparam>
/// <typeparam name="TResponse">The streamed response type.</typeparam>
public class StreamLoggingBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    private readonly ILogger<StreamLoggingBehavior<TRequest, TResponse>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="StreamLoggingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public StreamLoggingBehavior(ILogger<StreamLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        StreamHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Start streaming: {RequestType}", typeof(TRequest).Name);

        await foreach (var item in next().WithCancellation(cancellationToken))
        {
            _logger.LogDebug("Streaming item: {Item}", item);
            yield return item;
        }

        _logger.LogInformation("End streaming: {RequestType}", typeof(TRequest).Name);
    }
}