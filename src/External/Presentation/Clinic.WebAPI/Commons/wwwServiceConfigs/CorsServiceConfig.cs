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
                policy
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:3001",
                        "https://clinic-project-fe-liart.vercel.app/",
                        "https://pclinic.ohayo.io.vn",
                        "https://pclinic-admin.ohayo.io.vn"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}
