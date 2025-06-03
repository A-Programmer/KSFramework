using FluentValidation;
using FluentValidation.Results;
using KSFramework.Messaging.Abstraction;
using KSFramework.Messaging.Behaviors;
using Microsoft.Extensions.Logging;
using Moq;

namespace KSFramework.UnitTests.Behaviors.RequestValidation;

/// <summary>
/// Unit tests for <see cref="RequestValidationBehavior{TRequest, TResponse}"/>.
/// </summary>
public class RequestValidationBehaviorTests
{
    public class TestRequest : IRequest<TestResponse> { }

    public class TestResponse
    {
        public string Message { get; set; } = "OK";
    }

    [Fact]
    public async Task Handle_WithNoValidators_CallsNext()
    {
        // Arrange
        var called = false;
        Task<TestResponse> Next() { called = true; return Task.FromResult(new TestResponse()); }

        var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

        var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
            Array.Empty<IValidator<TestRequest>>(),
            loggerMock.Object
        );

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, Next);

        // Assert
        Assert.True(called);
        Assert.Equal("OK", result.Message);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var failures = new List<ValidationFailure> { new("Name", "Name is required") };
        var validationResult = new ValidationResult(failures);

        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock.Setup(x => x.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(validationResult);

        var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

        var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
            new[] { validatorMock.Object },
            loggerMock.Object
        );

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() =>
            behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse()))
        );

        Assert.Contains("Name is required", ex.Message);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_LogsWarning()
    {
        // Arrange
        var failures = new List<ValidationFailure> { new("Field", "must not be empty") };
        var validationResult = new ValidationResult(failures);

        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(validationResult);

        var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

        var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
            new[] { validatorMock.Object },
            loggerMock.Object
        );

        // Act
        await Assert.ThrowsAsync<ValidationException>(() =>
            behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse()))
        );

        // Assert log
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("must not be empty")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.Once
        );
    }
}