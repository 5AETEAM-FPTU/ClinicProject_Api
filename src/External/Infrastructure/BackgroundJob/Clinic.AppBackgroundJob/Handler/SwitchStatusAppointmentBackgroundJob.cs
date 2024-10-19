using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Clinic.AppBackgroundJob.Handler;

/// <summary>
///     Switch Status Appointment Background Job.
/// </summary>
public sealed class SwitchStatusAppointmentBackgroundJob : BackgroundService
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
