using System.Reflection;
using Faradars.Application.Services;
using Faradars.Application.Services.Validation;
using Faradars.Domain.Interfaces;
using Faradars.Infrastructure.Persistence.DbContext;
using Faradars.Shared.Settings;
using Faradars.WebApi.Common;

namespace Faradars.WebApi.Configurations;
public static class Conf
{
    public static Assembly[] GetAllAssemblies()
    {
        var commonAssembly = typeof(Setting).Assembly;
        var entitiesAssembly = typeof(IEntity).Assembly;
        var dataAssembly = typeof(ApplicationDbContext).Assembly;
        var servicesAssembly = typeof(BaseService).Assembly;
        var frameworkAssembly = typeof(UserBaseController).Assembly;
        var applicationAssembly = typeof(FluentValidatorService).Assembly;
        return
        [
            commonAssembly,
            entitiesAssembly,
            dataAssembly,
            servicesAssembly,
            frameworkAssembly,
            applicationAssembly
        ];
    }
}
