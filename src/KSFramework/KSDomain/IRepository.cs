using KSFramework.KSDomain.AggregatesHelper;

namespace KSFramework.KSDomain;

public interface IRepository<T> where T : IAggregateRoot
{
}

