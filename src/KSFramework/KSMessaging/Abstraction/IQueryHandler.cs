using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSMessaging.Abstraction;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit>
    where TQuery : IQuery
{
}