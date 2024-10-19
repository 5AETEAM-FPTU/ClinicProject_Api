namespace Clinic.AppBackgroundJob.Handler;

using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
///     Cancel ChatRoom BackgroundJ Job.
/// </summary>
public sealed class CancelChatRoomBackgroundJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CancelChatRoomBackgroundJob(IServiceScopeFactory serviceProviderFactory) =>
        _serviceScopeFactory = serviceProviderFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = DateTime.UtcNow.AddDays(1).Subtract(DateTime.UtcNow);

            await Task.Delay(
                millisecondsDelay: (int)delay.TotalMilliseconds,
                cancellationToken: stoppingToken
            );

            using var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var dbResult = repository.SwitchToCancelChatRoomRepository.SwitchToCancelChatRoom(
                cancellationToken: stoppingToken
            );
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"Daily task(Switch status appointment to no show) is running at: {DateTime.Now}"
            );
            Console.ForegroundColor = originalColor;

            await Task.Delay(
                millisecondsDelay: (int)TimeSpan.FromDays(1).TotalMilliseconds,
                cancellationToken: stoppingToken
            );
        }
    }
}
