using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Behaviors;
using Microsoft.Extensions.Logging;
using Moq;

namespace KSFramework.UnitTests.Behaviors.RequestProcessing;

/// <summary>
/// Unit tests for <see cref="RequestProcessorBehavior{TRequest, TResponse}"/>.
/// </summary>
public class RequestProcessorBehaviorTests
{
    public class TestRequest : IRequest<string> { }

    [Fact]
    public async Task Handle_WhenNoProcessors_ShouldCallHandlerDirectly()
    {
        // Arrange
        var logger = new Mock<ILogger<RequestProcessorBehavior<TestRequest, string>>>();
        var behavior = new RequestProcessorBehavior<TestRequest, string>(
            Enumerable.Empty<IRequestPreProcessor<TestRequest>>(),
            Enumerable.Empty<IRequestPostProcessor<TestRequest, string>>(),
            logger.Object);

        var wasCalled = false;
        RequestHandlerDelegate<string> next = () =>
        {
            wasCalled = true;
            return Task.FromResult("Hello");
        };

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, next);

        // Assert
        Assert.Equal("Hello", result);
        Assert.True(wasCalled);
    }

    [Fact]
    public async Task Handle_WithPreProcessor_ShouldInvokeBeforeHandler()
    {
        // Arrange
        var preProcessorMock = new Mock<IRequestPreProcessor<TestRequest>>();
        var postProcessors = Enumerable.Empty<IRequestPostProcessor<TestRequest, string>>();

        var logger = new Mock<ILogger<RequestProcessorBehavior<TestRequest, string>>>();

        var behavior = new RequestProcessorBehavior<TestRequest, string>(
            new[] { preProcessorMock.Object },
            postProcessors,
            logger.Object);

        RequestHandlerDelegate<string> next = () => Task.FromResult("Handled");

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, next);

        // Assert
        Assert.Equal("Handled", result);
        preProcessorMock.Verify(x => x.Process(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithPostProcessor_ShouldInvokeAfterHandler()
    {
        // Arrange
        var postProcessorMock = new Mock<IRequestPostProcessor<TestRequest, string>>();
        var preProcessors = Enumerable.Empty<IRequestPreProcessor<TestRequest>>();

        var logger = new Mock<ILogger<RequestProcessorBehavior<TestRequest, string>>>();

        var behavior = new RequestProcessorBehavior<TestRequest, string>(
            preProcessors,
            new[] { postProcessorMock.Object },
            logger.Object);

        RequestHandlerDelegate<string> next = () => Task.FromResult("Done");

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, next);

        // Assert
        Assert.Equal("Done", result);
        postProcessorMock.Verify(x =>
            x.Process(It.IsAny<TestRequest>(), "Done", It.IsAny<CancellationToken>()), Times.Once);
    }
}