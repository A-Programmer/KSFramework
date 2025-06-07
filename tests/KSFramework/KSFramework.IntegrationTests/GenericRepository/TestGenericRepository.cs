using KSFramework.GenericRepository;

namespace KSFramework.IntegrationTests.GenericRepository;

public class TestGenericRepository(TestDbContext context) : GenericRepository<TestEntity>(context);