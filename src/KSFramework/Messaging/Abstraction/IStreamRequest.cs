namespace KSFramework.Messaging.Abstraction;

/// <summary>
/// Marker interface for a request that returns a stream of responses.
/// </summary>
/// <typeparam name="TResponse">Type of the streamed response.</typeparam>
public interface IStreamRequest<TResponse> : IRequest<IAsyncEnumerable<TResponse>> { }