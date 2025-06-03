namespace KSFramework.Messaging.Abstraction;

/// <summary>
/// Represents a mediator that supports sending requests and publishing notifications.
/// </summary>
public interface IMediator : ISender
{
    /// <summary>
    /// Publishes a notification to all registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">The notification type.</typeparam>
    /// <param name="notification">The notification instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
    
    /// <summary>
    /// Sends a request without knowing the response type at compile time.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the response.</returns>
    Task<object?> Send(object request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Publishes a notification without knowing the notification type at compile time.
    /// </summary>
    /// <param name="notification">The notification instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Publish(object notification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a stream of responses for a given stream request.
    /// </summary>
    /// <typeparam name="TResponse">Type of the streamed response.</typeparam>
    /// <param name="request">The stream request instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An asynchronous stream of responses.</returns>
    IAsyncEnumerable<TResponse> CreateStream<TResponse>(
        IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = default);
}
