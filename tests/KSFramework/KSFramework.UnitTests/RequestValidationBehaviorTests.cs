using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using KSFramework.KSMessaging.Behaviors;
using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.UnitTests
{
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
            var nextCalled = false;
            Task<TestResponse> Next()
            {
                nextCalled = true;
                return Task.FromResult(new TestResponse());
            }

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                Array.Empty<IValidator<TestRequest>>(),
                loggerMock.Object);

            var result = await behavior.Handle(new TestRequest(), CancellationToken.None, Next);

            Assert.True(nextCalled);
            Assert.Equal("OK", result.Message);
        }

        [Fact]
        public async Task Handle_WithValidRequest_CallsNext()
        {
            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new ValidationResult()); // no errors

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                new[] { validatorMock.Object },
                loggerMock.Object);

            var nextCalled = false;
            Task<TestResponse> Next()
            {
                nextCalled = true;
                return Task.FromResult(new TestResponse());
            }

            var result = await behavior.Handle(new TestRequest(), CancellationToken.None, Next);

            Assert.True(nextCalled);
            Assert.Equal("OK", result.Message);
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ThrowsValidationException()
        {
            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new ValidationResult(new[]
                         {
                             new ValidationFailure("Prop", "Error")
                         }));

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                new[] { validatorMock.Object },
                loggerMock.Object);

            await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse())));
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ThrowsValidationException_ContainsErrorMessage()
        {
            var invalidFailures = new[]
            {
                new ValidationFailure("Name", "Name is required")
            };

            var validationResult = new ValidationResult(invalidFailures);

            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                new[] { validatorMock.Object },
                loggerMock.Object);

            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse())));

            Assert.Contains("Name is required", ex.Message);
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_LogsWarning()
        {
            // Arrange
            var invalidFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name is required")
            };

            var validationResult = new ValidationResult(invalidFailures);

            var validatorMock = new Mock<IValidator<TestRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                new[] { validatorMock.Object },
                loggerMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse())));

            Assert.Contains("Name is required", ex.Message);

            // Verify that a Warning log was written containing the validation error message
            loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Warning),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Name is required")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithMultipleValidators_AggregatesErrors()
        {
            var validator1 = new Mock<IValidator<TestRequest>>();
            validator1.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new[]
                {
                    new ValidationFailure("Name", "Name is required")
                }));

            var validator2 = new Mock<IValidator<TestRequest>>();
            validator2.Setup(v => v.ValidateAsync(It.IsAny<TestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new[]
                {
                    new ValidationFailure("Email", "Email is invalid")
                }));

            var loggerMock = new Mock<ILogger<RequestValidationBehavior<TestRequest, TestResponse>>>();

            var behavior = new RequestValidationBehavior<TestRequest, TestResponse>(
                new[] { validator1.Object, validator2.Object },
                loggerMock.Object);

            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                behavior.Handle(new TestRequest(), CancellationToken.None, () => Task.FromResult(new TestResponse())));

            Assert.Contains("Name is required", ex.Message);
            Assert.Contains("Email is invalid", ex.Message);
        }
    }
}