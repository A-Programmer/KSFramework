using KSFramework.GenericRepository;
using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.IntegrationTests.GenericRepository;

public class TestEntity : Entity, KSDomain.AggregatesHelper.IAggregateRoot
{
    private TestEntity(Guid id) : base(id)
    {
    }

    private TestEntity(Guid id, string name)
        :base(id)
    {
        Name = name;
    }

    public static TestEntity Create(Guid id, string name)
    {
        TestEntity testEntity = new(id, name);
        
        return testEntity;
    }

    public string Name { get; private set; } = String.Empty;
}

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<TestEntity> TestEntities { get; set; } = null!;
}

public class GenericRepositoryTests : IntegrationTestBase
{
    public GenericRepositoryTests()
    {
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddScoped<IGenericRepository<TestEntity>, TestGenericRepository>();
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntityToDatabase()
    {
        using var scope = CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Arrange
        var entity = TestEntity.Create(Guid.NewGuid(), "Test Entity");

        // Act
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();

        // Assert
        var savedEntity = await dbContext.TestEntities.FirstOrDefaultAsync(e => e.Id == entity.Id);
        Assert.NotNull(savedEntity);
        Assert.Equal("Test Entity", savedEntity.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity()
    {
        using var scope = CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
        var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Arrange
        var entity = TestEntity.Create(Guid.NewGuid(), "Test Entity");
        
        await dbContext.TestEntities.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal("Test Entity", result.Name);
    }
}