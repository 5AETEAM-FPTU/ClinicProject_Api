using Clinic.Application.Commons.Caching;
using Clinic.Redis.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Redis.wwwServiceConfigs;

/// <summary>
///     Core service config.
/// </summary>
internal static class CoreServiceConfig
{
    internal static void ConfigCore(this IServiceCollection services)
    {
        services.AddScoped<ICacheHandler, CacheHandler>();
    }
}
