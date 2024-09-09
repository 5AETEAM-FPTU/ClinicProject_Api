using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Application.Commons.wwwServiceConfigs;

/// <summary>
///     Fast endpoint service config.
/// </summary>
internal static class FastEndpointServiceConfig
{
    internal static void ConfigFastEndpoint(this IServiceCollection services)
    {
        services.AddFastEndpoints();
    }
}
