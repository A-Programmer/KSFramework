using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Extensions;
using KSFramework.KSMessaging.Samples;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Configuration;

public class AddKSMediatorTests
{
    [Fact]
    public async Task AddKSMediator_RegistersHandlersAndBehaviorsFromAssembly()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddKSMediator(typeof(MultiplyByTwoRequest).Assembly);

        var provider = services.BuildServiceProvider();

        // Act
        var handler = provider.GetService<IRequestHandler<MultiplyByTwoRequest, int>>();

        // Assert
        Assert.NotNull(handler);

        var result = await handler.Handle(new MultiplyByTwoRequest(5), CancellationToken.None);
        Assert.Equal(10, result);
    }
}