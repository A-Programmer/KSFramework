namespace KSFramework.Messaging;

/// <summary>
/// Represents a sender that can send requests and return responses.
/// </summary>
public interface ISender
{
    /// <summary>
    /// Sends a request asynchronously and returns the response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request">The request to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the request handler.</returns>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}