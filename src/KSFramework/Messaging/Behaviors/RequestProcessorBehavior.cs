using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.Logging;

namespace KSFramework.Messaging.Behaviors;

/// <summary>
/// Executes pre-processors and post-processors for the request.
/// </summary>
public class RequestProcessorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IRequestPreProcessor<TRequest>> _preProcessors;
    private readonly IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
    private readonly ILogger<RequestProcessorBehavior<TRequest, TResponse>> _logger;

    public RequestProcessorBehavior(
        IEnumerable<IRequestPreProcessor<TRequest>> preProcessors,
        IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors,
        ILogger<RequestProcessorBehavior<TRequest, TResponse>> logger)
    {
        _preProcessors = preProcessors;
        _postProcessors = postProcessors;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        foreach (var processor in _preProcessors)
        {
            _logger.LogInformation("Running preprocessor {Processor} for {RequestType}", processor.GetType().Name, typeof(TRequest).Name);
            await processor.Process(request, cancellationToken);
        }

        var response = await next();

        foreach (var processor in _postProcessors)
        {
            _logger.LogInformation("Running postprocessor {Processor} for {RequestType}", processor.GetType().Name, typeof(TRequest).Name);
            await processor.Process(request, response, cancellationToken);
        }

        return response;
    }
}