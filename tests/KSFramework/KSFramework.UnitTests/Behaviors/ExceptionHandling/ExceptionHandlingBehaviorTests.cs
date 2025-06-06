using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Behaviors;
using Microsoft.Extensions.Logging;
using Moq;

namespace KSFramework.UnitTests.Behaviors.ExceptionHandling;

/// <summary>
/// Unit tests for <see cref="ExceptionHandlingBehavior{TRequest, TResponse}"/>.
/// </summary>
public class ExceptionHandlingBehaviorTests
{
    public class TestRequest : IRequest<string> { }

    [Fact]
    public async Task Handle_WhenNoException_ReturnsResponse()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ExceptionHandlingBehavior<TestRequest, string>>>();

        var behavior = new ExceptionHandlingBehavior<TestRequest, string>(loggerMock.Object);

        RequestHandlerDelegate<string> next = () => Task.FromResult("OK");

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, next);

        // Assert
        Assert.Equal("OK", result);
        loggerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_WhenExceptionThrown_LogsAndRethrows()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ExceptionHandlingBehavior<TestRequest, string>>>();

        var behavior = new ExceptionHandlingBehavior<TestRequest, string>(loggerMock.Object);

        var exception = new InvalidOperationException("Something went wrong");

        RequestHandlerDelegate<string> next = () => throw exception;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            behavior.Handle(new TestRequest(), CancellationToken.None, next));

        Assert.Equal("Something went wrong", ex.Message);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Exception caught")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.Once
        );
    }
}