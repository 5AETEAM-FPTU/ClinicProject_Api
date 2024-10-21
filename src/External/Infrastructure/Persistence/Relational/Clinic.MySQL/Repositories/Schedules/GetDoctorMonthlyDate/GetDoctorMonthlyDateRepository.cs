using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.GetDoctorMonthlyDate;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Schedules.GetDoctorMonthlyDate;

public class GetDoctorMonthlyDateRepository : IGetDoctorMonthlyDateRepository
{
    private readonly ClinicContext _clinicContext;
    private readonly DbSet<Schedule> _schedules;

    public GetDoctorMonthlyDateRepository(ClinicContext clinicContext)
    {
        _clinicContext = clinicContext;
        _schedules = _clinicContext.Set<Schedule>();
    }

    public async Task<IEnumerable<Schedule>> GetScheduleDatesByMonthQueryAsync(
        int year,
        int month,
        Guid doctorId,
        CancellationToken cancellationToken = default
    )
    {
        return await _schedules
            .Where(schedule =>
                schedule.DoctorId == doctorId
                && schedule.StartDate.Year == year
                && schedule.StartDate.Month == month
            )
            .ToListAsync(cancellationToken);
    }

    public async Task<User> GetUserByDoctorId(Guid doctorId, CancellationToken cancellationToken)
    {
        return await _clinicContext.Users.FirstOrDefaultAsync(
            u => u.Id == doctorId,
            cancellationToken
        );
    }
}
