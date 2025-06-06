using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.KSMessaging.Samples;

public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredNotification>
{
    public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Email] Welcome email sent to {notification.Username}");
        return Task.CompletedTask;
    }
}

public class LogUserRegistrationHandler : INotificationHandler<UserRegisteredNotification>
{
    public Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Log] User '{notification.Username}' has registered.");
        return Task.CompletedTask;
    }
}