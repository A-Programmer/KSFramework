using KSFramework.Messaging.Abstraction;

namespace KSFramework.UnitTests.Mediator.Publish;

public class SampleNotification : INotification
{
    public string Message { get; set; }
}