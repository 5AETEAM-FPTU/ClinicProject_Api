using Clinic.AppBackgroundJob.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Clinic.AppBackgroundJob;

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
    public static void ConfigAppBackgroundJob(this IServiceCollection services)
    {
        services.AddHostedService<KeepAppAliveBackgroundJob>();
        services.AddHostedService<CancelAppointmentBackgroundJob>();
        services.AddHostedService<ScanAppointmentBackgroundJob>();
    }
}
