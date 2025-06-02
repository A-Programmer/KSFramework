using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging;

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
}
