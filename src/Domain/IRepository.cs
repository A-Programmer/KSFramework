using KSFramework.Primitives;

namespace KSFramework.Domain;

public interface IRepository<T> where T :  AggregateRoot
{
}

