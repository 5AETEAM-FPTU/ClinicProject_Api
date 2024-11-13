using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.CreateSchedules;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Schedules.CreateSchedules;

/// <summary>
///    Implement of ICreateSchedules repository.
/// </summary>
internal class CreateSchedulesRepository : ICreateSchedulesRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public CreateSchedulesRepository(ClinicContext context)
    {
        _context = context;
        _schedules = _context.Set<Schedule>();
    }

    public async Task<bool> AreOverLappedSlotTimes(
        IEnumerable<Schedule> createSchedules,
        CancellationToken cancellationToken = default
    )
    {
        var maxTime = createSchedules.Max(cs => cs.EndDate);
        var minTime = createSchedules.Min(cs => cs.StartDate);

        var existSlotTimes = await _schedules
            .Where(predicate: schedule =>
                schedule.StartDate < maxTime
                && schedule.EndDate > minTime
                && schedule.DoctorId == createSchedules.FirstOrDefault().DoctorId
            )
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var newSlot in createSchedules)
        {
            foreach (var existingSlot in existSlotTimes)
            {
                if (
                    existingSlot.StartDate < newSlot.EndDate
                    && newSlot.StartDate < existingSlot.EndDate
                )
                {
                    return true;
                }
            }
        }

        return false;
    }

    public async Task<bool> CreateSchedulesAsync(
        IEnumerable<Schedule> createSchedules,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _schedules.AddRange(createSchedules);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.ToString());
            return false;
        }

        return true;
    }
}
