namespace KSFramework.Messaging.Abstraction;

/// <summary>
/// Delegate that represents the next notification handler or behavior in the pipeline.
/// </summary>
public delegate Task NotificationHandlerDelegate();

/// <summary>
/// Interface for notification pipeline behaviors.
/// </summary>
/// <typeparam name="TNotification">Type of the notification.</typeparam>
public interface INotificationBehavior<TNotification> where TNotification : INotification
{
    Task Handle(TNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next);
}