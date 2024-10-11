using Clinic.Application.Commons.SMS;
using Clinic.TwilioSMS.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.TwilioSMS;

/// <summary>
///     Configure services for smtp layer.
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
    ///     Configuration manager.
    /// </param>
    public static void ConfigTwilioSmsNotification(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSingleton<ISmsHandler, TwilioSmsHandler>();
    }
}
