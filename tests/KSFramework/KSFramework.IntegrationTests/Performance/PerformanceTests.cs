using System.Diagnostics;
using KSFramework.GenericRepository;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.IntegrationTests.GenericRepository;
using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.IntegrationTests.Performance;

public class PerformanceTests : IntegrationTestBase
{
    private readonly IGenericRepository<TestEntity> _repository;
    private readonly TestDbContext _dbContext;
    private readonly Stopwatch _stopwatch;
    private readonly IUnitOfWork _unitOfWork;

    public PerformanceTests()
    {
        using var scope = CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
        _repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
        _stopwatch = new Stopwatch();
        _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddDbContext<TestDbContext>(options =>
            options.UseInMemoryDatabase("PerformanceTestDb"));

        services.AddScoped<IGenericRepository<TestEntity>, GenericRepository<TestEntity>>();
    }

    [Fact]
    public async Task BulkInsert_ShouldCompleteWithinTimeLimit()
    {
        // Arrange
        const int numberOfEntities = 1000;
        const int acceptableMilliseconds = 2000; // 2 seconds
        var entities = Enumerable.Range(1, numberOfEntities)
            .Select(i => new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = $"Performance Test Entity {i}"
            })
            .ToList();

        // Act
        _stopwatch.Start();

        foreach (var entity in entities)
        {
            await _repository.AddAsync(entity);
        }
        await _unitOfWork.SaveChangesAsync();

        _stopwatch.Stop();

        // Assert
        Assert.True(_stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Bulk insert took {_stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }

    [Fact]
    public async Task QueryPerformance_ShouldCompleteWithinTimeLimit()
    {
        // Arrange
        const int numberOfQueries = 100;
        const int acceptableMilliseconds = 1000; // 1 second
        var entity = new TestEntity
        {
            Id = Guid.NewGuid(),
            Name = "Performance Query Test Entity"
        };
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        // Act
        _stopwatch.Start();

        for (int i = 0; i < numberOfQueries; i++)
        {
            await _repository.GetByIdAsync(entity.Id);
        }

        _stopwatch.Stop();

        // Assert
        Assert.True(_stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Multiple queries took {_stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }

    [Fact]
    public async Task ConcurrentOperations_ShouldHandleEfficiently()
    {
        // Arrange
        const int numberOfTasks = 50;
        const int acceptableMilliseconds = 3000; // 3 seconds
        var tasks = new List<Task>();

        // Act
        _stopwatch.Start();

        for (int i = 0; i < numberOfTasks; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                using (var scope = CreateScope())
                {
                    var scopedUnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var scopedRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();

                    var entity = new TestEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Concurrent Test Entity {Guid.NewGuid()}"
                    };
                    await scopedRepository.AddAsync(entity);
                    await scopedUnitOfWork.SaveChangesAsync();
                    await scopedRepository.GetByIdAsync(entity.Id);
                }
            }));
        }

        await Task.WhenAll(tasks);
        _stopwatch.Stop();

        // Assert
        Assert.True(_stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Concurrent operations took {_stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }
}