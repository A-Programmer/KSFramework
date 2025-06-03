using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Mediator.Registration;

/// <summary>
/// Unit tests for AddKSMediator service registration.
/// </summary>
public class AddKSMediatorRegistrationTests
{
    public class TestRequest : IRequest<string> { }

    public class TestRequestHandler : IRequestHandler<TestRequest, string>
    {
        public Task<string> Handle(TestRequest request, CancellationToken cancellationToken) =>
            Task.FromResult("Handled");
    }

    public class TestNotification : INotification { }

    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        public bool Handled { get; private set; } = false;
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }

    public class TestRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return await next();
        }
    }

    public class TestNotificationBehavior : INotificationBehavior<TestNotification>
    {
        public bool WasCalled { get; private set; } = false;

        public async Task Handle(TestNotification notification, CancellationToken cancellationToken, NotificationHandlerDelegate next)
        {
            WasCalled = true;
            await next();
        }
    }

    [Fact]
    public async Task AddKSMediator_RegistersHandlersAndBehaviorsCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddKSMediator(typeof(AddKSMediatorRegistrationTests).Assembly);
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetService<IMediator>();
        Assert.NotNull(mediator);

        // Act
        var response = await mediator.Send(new TestRequest());
        await mediator.Publish(new TestNotification());

        // Assert
        Assert.Equal("Handled", response);
    }
}