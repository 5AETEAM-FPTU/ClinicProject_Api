using Clinic.Domain.Commons.Others;
using Clinic.Domain.Features.UnitOfWorks;
using Clinic.MySQL.Repositories.Auths.Authorization;
using Clinic.MySQL.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.MySQL.wwwServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
