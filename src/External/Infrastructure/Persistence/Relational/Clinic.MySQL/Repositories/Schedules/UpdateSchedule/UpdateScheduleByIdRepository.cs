using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.UpdateSchedule;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public async Task<bool> AreOverLappedSchedule(Guid doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var existSchedules = await _schedules
            .Where(predicate: schedule => schedule.DoctorId == doctorId)
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

    public Task<bool> IsScheduleExist(Guid doctorId, Guid scheduleId)
    {
        return _schedules
            .AnyAsync(schedule => schedule.DoctorId == doctorId && schedule.Id == scheduleId);
    }

    public async Task<bool> UpdateScheduleByIdCommandAsync(Guid doctorId, Guid scheduleId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var schedule = await _schedules
            .Where(schedule => schedule.DoctorId == doctorId)
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
