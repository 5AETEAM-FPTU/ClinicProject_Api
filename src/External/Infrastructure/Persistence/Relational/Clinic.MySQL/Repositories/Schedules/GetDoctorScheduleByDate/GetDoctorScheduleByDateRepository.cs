using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.GetDoctorScheduleByDate;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Schedules.GetDoctorScheduleByDate;

public class GetDoctorScheduleByDateRepository : IGetDoctorScheduleByDateRepository
{
    private readonly ClinicContext _clinicContext;
    private readonly DbSet<Schedule> _schedules;

    public GetDoctorScheduleByDateRepository(ClinicContext clinicContext)
    {
        _clinicContext = clinicContext;
        _schedules = _clinicContext.Set<Schedule>();
    }

    public async Task<IEnumerable<Schedule>> GetSchedulesByDateQueryAsync(
        Guid doctorId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default
    )
    {
        return await _schedules
            .Where(entity =>
                entity.StartDate >= startDate
                && entity.EndDate <= endDate
                && entity.DoctorId == doctorId
            )
            .Select(entity => new Schedule()
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Appointment =
                    entity.Appointment.ScheduleId == entity.Id ? entity.Appointment : null,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetUserByDoctorId(Guid doctorId, CancellationToken cancellationToken)
    {
        return await _clinicContext
            .Users.Include(u => u.Doctor)
            .FirstOrDefaultAsync(u => u.Id == doctorId, cancellationToken);
    }
}
