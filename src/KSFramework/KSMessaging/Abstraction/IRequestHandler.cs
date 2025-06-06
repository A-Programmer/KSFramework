namespace KSFramework.KSMessaging.Abstraction;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IRequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
    where TRequest : IRequest
{
}
