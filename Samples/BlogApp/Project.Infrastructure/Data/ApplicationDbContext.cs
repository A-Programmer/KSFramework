using KSFramework.KSDomain;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Services;
using System.Linq;

namespace Project.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IServiceProvider serviceProvider)
        : base(options)
    {
        _serviceProvider = serviceProvider;
        _dispatcher = _serviceProvider.GetRequiredService<IDomainEventDispatcher>();
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = Domain.AssemblyReference.Assembly;

        #region Register All Entities
        modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        #endregion
        
        // TODO: Fix this by using modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        #region Apply Entities Configuration
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        #endregion

        #region Config Delete Behevior for not Cascade Delete
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        #endregion

        #region Add Sequential GUID for Id properties
        modelBuilder.AddSequentialGuidForIdConvention();
        #endregion

        #region Pluralize Table Names
        modelBuilder.AddPluralizingTableNameConvention();
        #endregion

        #region Data Seeder

        // modelBuilder.Seed();

        #endregion
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<BaseEntity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Any())
        {
            await _dispatcher.DispatchEventsAsync(domainEvents, cancellationToken);

            // Clear domain events after dispatching
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.ClearDomainEvents();
            }
        }

        return result;
    }
}