using MediatR;

namespace KSFramework.Primitives;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}