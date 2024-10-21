using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Schedules.GetScheduleDatesByMonth;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Schedules.GetSchedulesDateByMonth;

public class GetScheduleDatesByMonthRepository : IGetScheduleDatesByMonthRepository
{
    private readonly ClinicContext _context;
    private DbSet<Schedule> _schedules;

    public GetScheduleDatesByMonthRepository(ClinicContext context)
    {
        _context = context;
        _schedules = _context.Set<Schedule>();
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context
            .Users.Include(u => u.Doctor) // Include the related Doctor entity
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<IEnumerable<Schedule>> GetScheduleDatesByMonthQueryAsync(
        int year,
        int month,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _schedules
            .Where(schedule =>
                schedule.DoctorId == userId
                && schedule.StartDate.Year == year
                && schedule.StartDate.Month == month
            )
            .ToListAsync(cancellationToken);
    }
}
