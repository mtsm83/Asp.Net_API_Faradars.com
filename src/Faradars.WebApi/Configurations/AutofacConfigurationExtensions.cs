using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Faradars.Application.Interfaces.General;
using Faradars.Infrastructure.Persistence.Repositories;
using Faradars.Shared.Settings;

namespace Faradars.WebApi.Configurations;

public static class AutofacConfigurationExtensions
{
    public static void AddServices(this ContainerBuilder containerBuilder, IConfiguration configuration, Setting setting)
    {
        containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
        
        containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .InstancePerDependency();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    #region Private Methods

    private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAllAssembly(
        this ContainerBuilder containerBuilder)
    {
        return containerBuilder.RegisterAssemblyTypes(Conf.GetAllAssemblies());
    }

    #endregion
}