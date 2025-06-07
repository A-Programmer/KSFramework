namespace KSFramework.KSMessaging.Abstraction;

public interface IRequest<TResponse>
{
}

public interface IRequest : IRequest<Unit> { }
