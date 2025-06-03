namespace KSFramework.Messaging.Abstraction;

/// <summary>
/// Defines a handler for a specific type of notification.
/// </summary>
/// <typeparam name="TNotification">The notification type.</typeparam>
public interface INotificationHandler<TNotification> where TNotification : INotification
{
    /// <summary>
    /// Handles the notification asynchronously.
    /// </summary>
    /// <param name="notification">The notification instance.</param>
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}
