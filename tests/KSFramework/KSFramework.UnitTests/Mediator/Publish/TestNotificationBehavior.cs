using KSFramework.Messaging.Abstraction;

namespace KSFramework.UnitTests.Mediator.Publish;

public class TestNotificationBehavior : INotificationBehavior<SampleNotification>
{
    public List<string> CallOrder { get; } = new();

    public Task Handle(SampleNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next)
    {
        CallOrder.Add("Before");
        var task = next();
        CallOrder.Add("After");
        return task;
    }
}