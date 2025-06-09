using System.Diagnostics;
using KSFramework.GenericRepository;
using KSFramework.IntegrationTests;
using KSFramework.IntegrationTests.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class PerformanceTests : IntegrationTestBase
{
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
        using var scope = CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var stopwatch = new Stopwatch();

        const int numberOfEntities = 1000;
        const int acceptableMilliseconds = 2000;
        var entities = Enumerable.Range(1, numberOfEntities)
            .Select(i => TestEntity.Create(Guid.NewGuid(), $"Performance Test Entity {i}"))
            .ToList();

        stopwatch.Start();
        foreach (var entity in entities)
        {
            await repository.AddAsync(entity);
        }
        await unitOfWork.SaveChangesAsync();
        stopwatch.Stop();

        Assert.True(stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Bulk insert took {stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }

    [Fact]
    public async Task QueryPerformance_ShouldCompleteWithinTimeLimit()
    {
        using var scope = CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var stopwatch = new Stopwatch();

        const int numberOfQueries = 100;
        const int acceptableMilliseconds = 1000;
        var entity = TestEntity.Create(Guid.NewGuid(), "Performance Query Test Entity");
        
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();

        stopwatch.Start();
        for (int i = 0; i < numberOfQueries; i++)
        {
            await repository.GetByIdAsync(entity.Id);
        }
        stopwatch.Stop();

        Assert.True(stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Multiple queries took {stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }

    [Fact]
    public async Task ConcurrentOperations_ShouldHandleEfficiently()
    {
        const int numberOfTasks = 50;
        const int acceptableMilliseconds = 3000;
        var stopwatch = new Stopwatch();
        var tasks = new List<Task>();

        stopwatch.Start();
        for (int i = 0; i < numberOfTasks; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                using var scope = CreateScope();
                var scopedRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<TestEntity>>();
                var scopedUnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var entity = TestEntity.Create(Guid.NewGuid(), $"Concurrent Test Entity {Guid.NewGuid()}");

                await scopedRepository.AddAsync(entity);
                await scopedUnitOfWork.SaveChangesAsync();
                await scopedRepository.GetByIdAsync(entity.Id);
            }));
        }
        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Assert.True(stopwatch.ElapsedMilliseconds <= acceptableMilliseconds,
            $"Concurrent operations took {stopwatch.ElapsedMilliseconds}ms, which is more than acceptable {acceptableMilliseconds}ms");
    }
}