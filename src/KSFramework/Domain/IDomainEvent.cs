using KSFramework.Messaging.Abstraction;

namespace KSFramework.Domain;
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}