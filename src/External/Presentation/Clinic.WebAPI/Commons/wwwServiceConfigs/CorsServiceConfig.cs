using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI.Commons.wwwServiceConfigs;

/// <summary>
///     Cors service config.
/// </summary>
internal static class CorsServiceConfig
{
    internal static void ConfigCors(this IServiceCollection services)
    {
        services.AddCors(setupAction: config =>
        {
            config.AddDefaultPolicy(configurePolicy: policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
    }
}
