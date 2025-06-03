using KSFramework.Messaging.Abstraction;

namespace KSFramework.UnitTests;

using KSFramework.Messaging.Behaviors;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class ExceptionHandlingBehaviorTests
{
    public class TestRequest : IRequest<string> { }

    [Fact]
    public async Task Handle_WhenHandlerThrowsException_ShouldLogAndRethrow()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ExceptionHandlingBehavior<TestRequest, string>>>();
        var behavior = new ExceptionHandlingBehavior<TestRequest, string>(loggerMock.Object);

        var request = new TestRequest();

        // next delegate that throws exception
        RequestHandlerDelegate<string> next = () => throw new InvalidOperationException("Test exception");

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => behavior.Handle(request, CancellationToken.None, next));

        // Verify that logger.LogError was called with the exception
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Exception caught")),
                ex,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}