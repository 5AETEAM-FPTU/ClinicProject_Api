namespace Clinic.AppBackgroundJob.Handler;

using Microsoft.Extensions.Hosting;

/// <summary>
///     Keep App Alive Background Job.
/// </summary>
public sealed class KeepAppAliveBackgroundJob : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Do work
            Console.WriteLine(value: "App is alive !!");

            await Task.Delay(millisecondsDelay: 120000, cancellationToken: stoppingToken);

            Console.Clear();
        }
    }
}
