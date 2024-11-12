using Clinic.AppBackgroundJob.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior =
                BackgroundServiceExceptionBehavior.Ignore;
        });
        services.AddHostedService<KeepAppAliveBackgroundJob>();
        services.AddHostedService<CancelAppointmentBackgroundJob>();
        services.AddHostedService<ScanAppointmentBackgroundJob>();
    }
}
