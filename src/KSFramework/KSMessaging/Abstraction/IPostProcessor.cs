namespace KSFramework.KSMessaging.Abstraction;

/// <summary>
/// Defines a post-processor that runs after the request handler.
/// </summary>
public interface IRequestPostProcessor<in TRequest, in TResponse>
{
    Task Process(TRequest request, TResponse response, CancellationToken cancellationToken);
}