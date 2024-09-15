using Clinic.Redis.wwwServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Redis;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    public static void ConfigRedisCachingDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigCore();
        services.ConfigStackExchangeRedisCache(configuration: configuration);
    }
}
