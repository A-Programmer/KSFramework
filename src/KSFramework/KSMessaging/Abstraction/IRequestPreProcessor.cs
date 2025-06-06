namespace KSFramework.KSMessaging.Abstraction;

/// <summary>
/// Defines a pre-processor that runs before the request handler.
/// </summary>
public interface IRequestPreProcessor<in TRequest>
{
    Task Process(TRequest request, CancellationToken cancellationToken);
}