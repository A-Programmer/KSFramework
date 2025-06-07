using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Samples;

public class TestNotification : INotification
{
    public string Message { get; set; } = default!;
}

public class TestNotificationHandler : INotificationHandler<TestNotification>
{
    public Task Handle(TestNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received notification: {notification.Message}");
        return Task.CompletedTask;
    }
}