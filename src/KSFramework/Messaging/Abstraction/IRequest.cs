namespace KSFramework.Messaging.Abstraction;

public interface IRequest<TResponse>
{
}

public interface IRequest : IRequest<Unit> { }
