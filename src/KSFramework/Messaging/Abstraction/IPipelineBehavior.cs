using System.Threading;
using System.Threading.Tasks;

namespace KSFramework.Messaging.Abstraction;
/// <summary>
/// Defines a pipeline behavior for requests that can run code before and after the handler is invoked.
/// </summary>
/// <typeparam name="TRequest">Type of the request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public interface IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Executes behavior around the handler invocation.
    /// </summary>
    /// <param name="request">The request instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="next">The next delegate in the pipeline, i.e., the handler or next behavior.</param>
    /// <returns>The response from the next delegate.</returns>
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
}

/// <summary>
/// Delegate representing the next step in the pipeline.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
/// <returns>A task that completes with the response.</returns>
public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();