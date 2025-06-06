using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KSFramework.GenericRepository;
using KSFramework.IntegrationTests.GenericRepository;

namespace KSFramework.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly Lazy<IServiceProvider> _serviceProviderLazy;
    protected readonly TestServer TestServer;

    protected IntegrationTestBase()
    {
        var webHostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                // Configure the application here if needed
            })
            .ConfigureServices(services =>
            {
                this.ConfigureServices(services);
            });

        TestServer = new TestServer(webHostBuilder);
        _serviceProviderLazy = new Lazy<IServiceProvider>(() => TestServer.Services);
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // Base configuration for all tests
        services.AddLogging();

        // Configure in-memory database for testing
        services.AddDbContext<TestDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestDatabase");
        });

        services.AddScoped(provider => (DbContext)provider.GetRequiredService<TestDbContext>());

        // Register UnitOfWork and IUnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    protected IServiceScope CreateScope()
    {
        return _serviceProviderLazy.Value.CreateScope();
    }

    protected T GetService<T>() where T : class
    {
        using var scope = CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    public void Dispose()
    {
        TestServer?.Dispose();
    }
}