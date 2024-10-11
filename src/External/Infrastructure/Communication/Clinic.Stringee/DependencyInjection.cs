using Clinic.Application.Commons.CallToken;
using Clinic.Configuration.Infrastructure.Stringee;
using Clinic.Stringee.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.Stringee;

public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void ConfigureStringeeService(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSingleton(configuration.GetRequiredSection("Stringee").Get<StringeeOption>());

        services.AddSingleton<ICallTokenHandler, SrtringeeAccessTokenHandler>();
    }
}
