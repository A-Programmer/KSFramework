namespace KSFramework.KSMessaging.Abstraction;

/// <summary>
/// Defines a pipeline behavior for stream requests that can run code before and after the handler is invoked.
/// </summary>
/// <typeparam name="TRequest">Type of the stream request.</typeparam>
/// <typeparam name="TResponse">Type of the stream response.</typeparam>
public interface IStreamPipelineBehavior<TRequest, TResponse> where TRequest : IStreamRequest<TResponse>
{
    /// <summary>
    /// Executes behavior around the stream handler invocation.
    /// </summary>
    /// <param name="request">The stream request instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="next">The next delegate in the pipeline, i.e., the stream handler or next behavior.</param>
    /// <returns>A stream of response elements.</returns>
    IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken, StreamHandlerDelegate<TResponse> next);
}

/// <summary>
/// Delegate representing the next step in the stream pipeline.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
public delegate IAsyncEnumerable<TResponse> StreamHandlerDelegate<TResponse>();