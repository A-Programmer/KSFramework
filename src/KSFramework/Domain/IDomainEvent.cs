using MediatR;

namespace KSFramework.Domain;
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}