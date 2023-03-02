namespace KSFramework.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private init; }
    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }
    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }
    public int Version { get; private set; } = 0;

    protected void IncreaseVersion()
    {
        Version++;
    }

    public override bool Equals(object? obj)
    {
        if (obj is nuint)
            return false;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not Entity entity)
            return false;

        return entity.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;
            
        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }
}