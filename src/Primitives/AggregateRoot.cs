using KSFramework.Domain;

namespace KSFramework.Primitives;

public class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents = new();
    
    protected AggregateRoot(Guid id) 
        : base(id)
    {
    }
    
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}