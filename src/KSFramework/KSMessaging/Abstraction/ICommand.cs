namespace KSFramework.KSMessaging.Abstraction;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}