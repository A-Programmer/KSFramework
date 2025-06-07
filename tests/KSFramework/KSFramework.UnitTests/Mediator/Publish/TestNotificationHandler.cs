using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.UnitTests.Mediator.Publish;


public class TestNotificationHandler : INotificationHandler<SampleNotification>
{
    public bool Handled { get; private set; } = false;

    public Task Handle(SampleNotification notification, CancellationToken cancellationToken)
    {
        Handled = true;
        return Task.CompletedTask;
    }
}