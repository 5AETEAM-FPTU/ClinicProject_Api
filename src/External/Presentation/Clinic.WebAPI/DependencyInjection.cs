using Clinic.WebAPI.Commons.wwwServiceConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.WebAPI;

/// <summary>
///     Entry to configuring multiple services
///     of web api.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    internal static void ConfigWebApi(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.ConfigCore(configuration: configuration);
    }
}
