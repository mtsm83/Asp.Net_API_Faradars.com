using System.Linq.Expressions;
using System.Reflection;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Services;
using Faradars.Domain.Interfaces;
using Faradars.Infrastructure.Persistence.Configurations;
using Faradars.Infrastructure.Persistence.Extensions;
using Faradars.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Infrastructure.Persistence.DbContext;


public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IUserContextService? _userContextService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IUserContextService userContext)
        : base(options)
    {
        _userContextService = userContext;
    }

    // 👇 constructor مخصوص migrations
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("dbo");
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeleteableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, new object[] { modelBuilder });
            }
        }


        var entitiesAssembly = typeof(IEntity).Assembly;
        var configurationAssembly = typeof(UserConfiguration).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);

        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly, configurationAssembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddSequentialGuidForIdConvention();
        modelBuilder.AddPluralizingTableNameConvention();
    }

    public override int SaveChanges()
    {
        CleanString();
        ApplyAuditInformation();
        ApplySoftDelete();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        CleanString();
        ApplyAuditInformation();
        ApplySoftDelete();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        CleanString();
        ApplyAuditInformation();
        ApplySoftDelete();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        CleanString();
        ApplyAuditInformation();
        ApplySoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }

    #region Private Methods

    private void CleanString()
    {
        var changedEntities = ChangeTracker.Entries().Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var item in changedEntities)
        {
            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x is { CanRead: true, CanWrite: true } && x.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var valObj = property.GetValue(item.Entity, null);

                if (valObj is not string val || string.IsNullOrWhiteSpace(val)) continue;

                var newVal = val.Fa2En().FixPersianChars();

                if (newVal == val)
                    continue;

                property.SetValue(item.Entity, newVal, null);
            }
        }
    }

    private void ApplyAuditInformation()
    {
        var dateTimeUtcNow = DateTime.UtcNow;
        var entries = ChangeTracker.Entries().Where(x =>
            (x.State is EntityState.Added or EntityState.Modified) && x.Entity is IAuditableEntity);
        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditableEntity auditableEntity)
            {
                if (entry.State == EntityState.Added)
                    auditableEntity.CreatedAt = dateTimeUtcNow;

                if (entry.State == EntityState.Modified)
                    auditableEntity.UpdatedAt = dateTimeUtcNow;
            }
        }
    }

    private void ApplySoftDelete()
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x is { State: EntityState.Deleted, Entity: ISoftDeleteableEntity });
        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            ((ISoftDeleteableEntity)entry.Entity).IsDeleted = true;
            ((ISoftDeleteableEntity)entry.Entity).DeletedAt = DateTime.UtcNow;
            ((ISoftDeleteableEntity)entry.Entity).DeletedBy = _userContextService?.CurrentUser.UserId;
        }
    }
    
    private static void SetSoftDeleteFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, ISoftDeleteableEntity
    {
        builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }

    #endregion
}