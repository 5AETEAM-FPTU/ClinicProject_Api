using Clinic.Domain.Features.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Clinic.AppBackgroundJob.Handler;

/// <summary>
///     Keep App Alive Background Job.
/// </summary>
public sealed class ScanAppointmentBackgroundJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ScanAppointmentBackgroundJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var originalColor = Console.ForegroundColor;

            using var scope = _serviceScopeFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var dbResult = await unitOfWork.RemoveAppointmentRepository.DeleteAppointment(
                cancellationToken: stoppingToken
            );

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = originalColor;

            await Task.Delay(
                millisecondsDelay: (int)TimeSpan.FromMinutes(5).TotalMilliseconds,
                cancellationToken: stoppingToken
            );

            //Console.Clear();
        }
    }
}
