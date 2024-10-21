using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.RemoveSchedule;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Schedules.RemoveSchedule;

public class RemoveScheduleRepository : IRemoveScheduleRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public RemoveScheduleRepository(ClinicContext context)
    {
        _context = context;
        _schedules = context.Set<Schedule>();
    }

    public Task<bool> IsScheduleExist(Guid scheduleId)
    {
        return _schedules
            .AnyAsync(schedule => schedule.Id == scheduleId);
    }

    public Task<bool> IsScheduleHadAppointment(Guid scheduleId, CancellationToken cancellationToken)
    {
        return _schedules
            .AnyAsync(schedule =>schedule.Id == scheduleId && schedule.Appointment != null);
    }

    public async Task<bool> RemoveScheduleByIdCommandAsync(Guid scheduleId, CancellationToken cancellationToken)
    {
        var schedule = await _schedules
            .FirstOrDefaultAsync(s => s.Id == scheduleId, cancellationToken);

        if (schedule == null)
        {
            return false;
        }

        // Remove schedule
        _schedules.Remove(schedule);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    } 
}
