using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Configuration;

/// <summary>
/// Tests for verifying notification handlers and behaviors registration through AddKSFramework.
/// </summary>
public class AddKSMediatorNotificationTests
{
    /// <summary>
    /// A test notification.
    /// </summary>
    public class TestNotification : INotification
    {
        public string Message { get; set; } = "";
    }

    /// <summary>
    /// A test handler for <see cref="TestNotification"/>.
    /// </summary>
    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        public bool Handled { get; private set; } = false;

        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// A test notification behavior that wraps the handler.
    /// </summary>
    public class TestNotificationBehavior : INotificationBehavior<TestNotification>
    {
        public bool WasCalled { get; private set; } = false;

        public Task Handle(TestNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next)
        {
            WasCalled = true;
            return next();
        }
    }

    [Fact]
    public async Task AddKSMediator_Should_Register_NotificationHandler_And_Behavior()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddKSFramework(typeof(TestNotification).Assembly);
        services.AddSingleton<INotificationHandler<TestNotification>, TestNotificationHandler>();
        services.AddSingleton<INotificationBehavior<TestNotification>, TestNotificationBehavior>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        var notification = new TestNotification { Message = "Hello" };

        // Act
        await mediator.Publish(notification);

        // Assert handler
        var handler = provider.GetRequiredService<INotificationHandler<TestNotification>>() as TestNotificationHandler;
        Assert.NotNull(handler);
        Assert.True(handler.Handled);

        // Assert behavior
        var behavior = provider.GetRequiredService<INotificationBehavior<TestNotification>>() as TestNotificationBehavior;
        Assert.NotNull(behavior);
        Assert.True(behavior.WasCalled);
    }
}