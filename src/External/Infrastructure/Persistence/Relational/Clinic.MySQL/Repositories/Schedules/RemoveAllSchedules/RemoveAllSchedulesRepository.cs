using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.RemoveAllSchedules;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Schedules.RemoveAllSchedules;

public class RemoveAllSchedulesRepository: IRemoveAllSchedulesRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public RemoveAllSchedulesRepository(ClinicContext context)
    {
        _context = context;
        _schedules = context.Set<Schedule>();
    }

    public async Task<bool> RemoveAllSchedulesByDateCommandAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken)
    {
        // handle date
        DateTime startDate = date.Date;
        DateTime endDate = date.Date.AddDays(1).AddTicks(-1);

        // find all schedule not having appointment on specific date
        var schedules = await _schedules
            .Where(schedule => schedule.DoctorId == doctorId &&
                        schedule.StartDate >= startDate &&
                        schedule.StartDate < endDate &&
                        schedule.Appointment == null
                        )
            .ToListAsync(cancellationToken);

        if (schedules.Count == 0)
        {
            return false;
        }

        // Remove all schedule not having appointment on specific date
        _schedules.RemoveRange(schedules);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
