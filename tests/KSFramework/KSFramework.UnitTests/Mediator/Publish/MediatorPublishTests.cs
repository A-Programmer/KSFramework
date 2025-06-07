using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Mediator.Publish;

/// <summary>
/// Unit tests for Mediator's Publish method.
/// </summary>
public class MediatorPublishTests
{
    /// <summary>
    /// A simple test notification.
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

        /// <summary>
        /// Handles the notification.
        /// </summary>
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// A test behavior for <see cref="TestNotification"/> that sets a flag when invoked.
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
    public async Task Publish_InvokesNotificationHandlers()
    {
        var services = new ServiceCollection();

        var handler = new TestNotificationHandler();

        services.AddSingleton<INotificationHandler<TestNotification>>(handler);
        services.AddLogging();
        services.AddSingleton<IMediator, KSFramework.KSMessaging.Mediator>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        var notification = new TestNotification { Message = "Hello" };

        await mediator.Publish(notification);

        Assert.True(handler.Handled);
    }

    [Fact]
    public async Task Publish_ExecutesNotificationBehavior()
    {
        var services = new ServiceCollection();

        var handler = new TestNotificationHandler();
        var behavior = new TestNotificationBehavior();

        services.AddSingleton<INotificationHandler<TestNotification>>(handler);
        services.AddSingleton<INotificationBehavior<TestNotification>>(behavior);
        services.AddLogging();
        services.AddSingleton<IMediator, KSFramework.KSMessaging.Mediator>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        var notification = new TestNotification { Message = "With Behavior" };

        await mediator.Publish(notification);

        Assert.True(handler.Handled);
        Assert.True(behavior.WasCalled);
    }
}