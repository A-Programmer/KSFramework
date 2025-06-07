using KSFramework.KSMessaging.Abstraction;

namespace KSFramework.KSDomain;
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}