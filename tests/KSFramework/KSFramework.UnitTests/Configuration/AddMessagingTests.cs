using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Configuration;
using KSFramework.Messaging.Samples;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.UnitTests.Configuration;

public class AddMessagingTests
{
    [Fact]
    public void Should_Register_All_Handlers_And_Behaviors_From_Assembly()
    {
        // Arrange
        var services = new ServiceCollection();

        // اینجا مهمه: باید اسمبلی‌ای رو بده که MultiplyByTwoHandler توشه
        services.AddMessaging(typeof(MultiplyByTwoRequest).Assembly);

        var provider = services.BuildServiceProvider();
        var handler = provider.GetService<IRequestHandler<MultiplyByTwoRequest, int>>();

        // Assert
        Assert.NotNull(handler);
        var result = handler.Handle(new MultiplyByTwoRequest(5), CancellationToken.None).Result;
        Assert.Equal(10, result);
    }
}