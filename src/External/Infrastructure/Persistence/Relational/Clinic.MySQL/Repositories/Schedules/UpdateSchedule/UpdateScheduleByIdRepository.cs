using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.UpdateSchedule;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Schedules.UpdateSchedule;

public class UpdateScheduleByIdRepository : IUpdateScheduleByIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public UpdateScheduleByIdRepository (ClinicContext context)
    {
        _context = context;
        _schedules = _context.Set<Schedule>();
    }

    public async Task<bool> AreOverLappedSchedule(Guid doctorId, Guid scheduleId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var existSchedules = await _schedules
            .Where(predicate: schedule => schedule.DoctorId == doctorId && schedule.Id != scheduleId)
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var schedule in existSchedules)
        {   
            if (schedule.StartDate < endDate
                    && startDate < schedule.EndDate)
            {
                return true;
            }    
        }
        return false;
    }

    public Task<bool> IsScheduleExist(Guid scheduleId)
    {
        return _schedules
            .AnyAsync(schedule => schedule.Id == scheduleId);
    }

    public async Task<bool> IsScheduleHadAppoitment(Guid scheduleId, CancellationToken cancellationToken)
    {
        return await _schedules
            .AnyAsync(schedule => schedule.Id == scheduleId && schedule.Appointment != null);
    }

    public async Task<bool> UpdateScheduleByIdCommandAsync(Guid scheduleId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var schedule = await _schedules
            .FirstOrDefaultAsync(schedule => schedule.Id == scheduleId, cancellationToken);

        if (schedule == null)
        {
            return false;
        }

        // update schedule
        schedule.StartDate = startDate;
        schedule.EndDate = endDate;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
