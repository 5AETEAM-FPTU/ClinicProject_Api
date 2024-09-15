using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.Commons.wwwServiceConfigs;

/// <summary>
///     Authorization service config.
/// </summary>
public static class AuthorizationServiceConfig
{
    internal static void ConfigAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}
