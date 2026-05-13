using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pluralize.NET;

namespace Faradars.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true } &&
                        typeof(TBaseType).IsAssignableFrom(c));

        foreach (var type in types)
        {
            modelBuilder.Entity(type);
        }
    }

    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var applyGenericMethod = typeof(ModelBuilder).GetMethods()
            .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies.SelectMany(a => a.GetExportedTypes()
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true }));

        foreach (var type in types)
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (iface.IsConstructedGenericType &&
                    iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(modelBuilder, [Activator.CreateInstance(type)]);
                }
            }
        }
    }

    public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        Pluralizer pluralizer = new();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            entityType.SetTableName(pluralizer.Pluralize(tableName));
        }
    }

    public static void AddSequentialGuidForIdConvention(this ModelBuilder modelBuilder)
    {
        modelBuilder.AddDefaultValueSqlConvention("Id", typeof(Guid), "NEWSEQUENTIALID()");
    }

    #region Private Methods

    private static void AddDefaultValueSqlConvention(this ModelBuilder modelBuilder, string propertyName,
        Type propertyType, string defaultValueSql)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var property = entityType.GetProperties()
                .SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            if (property != null && property.ClrType == propertyType)
                property.SetDefaultValueSql(defaultValueSql);
        }
    }

    #endregion
}