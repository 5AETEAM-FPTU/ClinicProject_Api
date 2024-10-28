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
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("App is alive !!");
            Console.ForegroundColor = originalColor;

            await Task.Delay(millisecondsDelay: 1200000, cancellationToken: stoppingToken);

            //Console.Clear();
        }
    }
}
