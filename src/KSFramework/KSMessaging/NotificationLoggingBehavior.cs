using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.KSMessaging;

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
        _logger.LogInformation($"[NotificationLogging] Handling {typeof(TNotification).Name} started.");
        await next();
        _logger.LogInformation($"[NotificationLogging] Handling {typeof(TNotification).Name} finished.");
    }
}