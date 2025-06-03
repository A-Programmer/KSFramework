using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Extensions;
using KSFramework.Messaging.Samples;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Configuration;

public class AddKSMediatorTests
{
    [Fact]
    public void AddKSMediator_RegistersHandlersAndBehaviorsFromAssembly()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddKSMediator(typeof(MultiplyByTwoRequest).Assembly);

        var provider = services.BuildServiceProvider();

        // Act
        var handler = provider.GetService<IRequestHandler<MultiplyByTwoRequest, int>>();

        // Assert
        Assert.NotNull(handler);

        var result = handler.Handle(new MultiplyByTwoRequest(5), CancellationToken.None).Result;
        Assert.Equal(10, result);
    }
}