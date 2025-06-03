using KSFramework.Messaging.Abstraction;

namespace KSFramework.Messaging.Behaviors;

/// <summary>
/// Executes pre-processors and post-processors for the request.
/// </summary>
public class RequestProcessorBehavior<TRequest, TResponse>(
    IEnumerable<IRequestPreProcessor<TRequest>> preProcessors,
    IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        foreach (var preProcessor in preProcessors)
            await preProcessor.Process(request, cancellationToken);

        var response = await next();

        foreach (var postProcessor in postProcessors)
            await postProcessor.Process(request, response, cancellationToken);

        return response;
    }
}