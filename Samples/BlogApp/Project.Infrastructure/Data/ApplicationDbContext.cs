using KSFramework.KSDomain;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Outbox;

namespace Project.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
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
}