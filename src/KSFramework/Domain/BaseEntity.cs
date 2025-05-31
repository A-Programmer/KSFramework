using System;
namespace KSFramework.Domain;
public interface IEntity
{

}

public abstract class BaseEntity : BaseEntity<Guid>
{
    public new Guid Id { get; set; } = Guid.NewGuid();
}

public abstract class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
    public int Version { get; private set; } = 0;

    private List<IDomainEvent> _domainEvents;
    public  IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    protected void IncreaseVersion()
    {
        Version++;
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}