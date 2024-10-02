using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.GetSchedulesByDate;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Schedules.GetSchedulesByDate;

/// <summary>
///    Implement of IGetSchedulesByDate repository.
/// </summary>
internal class GetSchedulesByDateRepository : IGetSchedulesByDateRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public GetSchedulesByDateRepository(ClinicContext context)
    {
        _context = context;
        _schedules = _context.Set<Schedule>();
    }

    public async Task<IEnumerable<Schedule>> GetSchedulesByDateQueryAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default
    )
    {
        return await _schedules
            .Where(entity => entity.StartDate >= startDate && entity.EndDate <= endDate)
            .Select(entity => new Schedule()
            {
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Appointment = entity.Appointment.ScheduleId == entity.Id ? entity.Appointment : null
            })
            .ToListAsync(cancellationToken);
    }
}
