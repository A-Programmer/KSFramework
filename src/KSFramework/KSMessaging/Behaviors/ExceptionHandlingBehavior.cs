using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.KSMessaging.Behaviors
{
    /// <summary>
    /// Behavior that catches exceptions during request handling, logs them, and rethrows.
    /// </summary>
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

        public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught in {Behavior} handling request {RequestType}", nameof(ExceptionHandlingBehavior<TRequest, TResponse>), typeof(TRequest).Name);
                throw;
            }
        }
    }
}