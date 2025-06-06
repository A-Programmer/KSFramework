using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Behaviors
{
    /// <summary>
    /// Pipeline behavior for validating requests using FluentValidation validators.
    /// Throws ValidationException if validation fails.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> Validators;
        private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> Logger;

        public RequestValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
        {
            Validators = validators;
            Logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (Validators.Any())
            {
                var validationResults = await Task.WhenAll(
                    Validators.Select(v => v.ValidateAsync(request, cancellationToken)));

                var failures = validationResults
                    .Where(r => r != null)
                    .SelectMany(r => r.Errors ?? Enumerable.Empty<ValidationFailure>())
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    Logger.LogWarning("Validation failed for {RequestType}: {Failures}", typeof(TRequest).Name, failures);
                    throw new ValidationException(failures);
                }
            }

            // If there is no validator or validation passes, continue handler execution
            return await next();
        }
    }
}