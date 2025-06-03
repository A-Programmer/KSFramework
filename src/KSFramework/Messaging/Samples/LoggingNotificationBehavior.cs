using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Samples;

public class LoggingNotificationBehavior<TNotification> : INotificationBehavior<TNotification> where TNotification : INotification
{
    public Task Handle(TNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next)
    {
        Console.WriteLine($"Before handling notification of type {typeof(TNotification).Name}");
        var task = next();
        Console.WriteLine($"After handling notification of type {typeof(TNotification).Name}");
        return task;
    }
}