namespace KSFramework.Primitives;

public abstract class AggregateRootWithSoftDelete : AggregateRoot
{
    protected AggregateRootWithSoftDelete(Guid id) : base(id)
    {
    }
    public bool IsDeleted { get; private set; } = false;

    public void Delete() => IsDeleted = true;

    public void Recover() => IsDeleted = false;
}