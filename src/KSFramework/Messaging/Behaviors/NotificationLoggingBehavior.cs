using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.Messaging.Behaviors;

public class NotificationLoggingBehavior<TNotification> : INotificationBehavior<TNotification> 
    where TNotification : INotification
{
    private readonly ILogger<NotificationLoggingBehavior<TNotification>> _logger;

    public NotificationLoggingBehavior(ILogger<NotificationLoggingBehavior<TNotification>> logger)
    {
        _logger = logger;
    }

    public async Task Handle(TNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next)
    {
        _logger.LogInformation("Handling notification of type {NotificationType}", typeof(TNotification).Name);

        await next();

        _logger.LogInformation("Handled notification of type {NotificationType}", typeof(TNotification).Name);
    }
}