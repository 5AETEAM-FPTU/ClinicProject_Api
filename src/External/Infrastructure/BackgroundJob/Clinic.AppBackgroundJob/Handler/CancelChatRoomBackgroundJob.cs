using Microsoft.Extensions.Hosting;

namespace Clinic.AppBackgroundJob.Handler;

/// <summary>
///     Cancel ChatRoom BackgroundJ Job.
/// </summary>
public sealed class CancelChatRoomBackgroundJob : BackgroundService
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
