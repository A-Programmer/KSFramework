using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Abstraction;

public interface IQuery : IRequest<Unit>
{
}

public interface IQuery<TResponse> : IRequest<TResponse>
{
}