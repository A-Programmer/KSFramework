using KSFramework.Domain.AggregatesHelper;

namespace KSFramework.Domain;

public interface IRepository<T> where T : IAggregateRoot
{
}

