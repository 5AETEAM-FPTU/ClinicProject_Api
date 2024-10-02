using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.Appointments.GetAppointmentUpcoming;

/// <summary>
///     Implementation of <see cref="IGetAppointmentUpcomingRepository"/>
/// </summary>
internal class GetAppointmentUpcomingRepository : IGetAppointmentUpcomingRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public GetAppointmentUpcomingRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public Task<DateTime> GetAppointmentUpcomingByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _appointments
            .AsNoTracking()
            .Where(entity => entity.PatientId == userId && entity.ExaminationDate > DateTime.Now)
            .OrderBy(entity => entity.ExaminationDate)
            .Select(entity => entity.ExaminationDate)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<int> GetTotalAppointmentedByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _appointments
            .Where(entity => entity.PatientId == userId && entity.ExaminationDate < DateTime.Now)
            .CountAsync(cancellationToken: cancellationToken);
    }
}
