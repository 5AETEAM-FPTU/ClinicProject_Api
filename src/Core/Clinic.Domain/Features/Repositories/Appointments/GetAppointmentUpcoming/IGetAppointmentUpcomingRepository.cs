using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.GetAppointmentUpcoming;

/// <summary>
///     Interface IGetAppointmentUpcomingRepository
/// </summary>
public interface IGetAppointmentUpcomingRepository
{
    Task<DateTime> GetAppointmentUpcomingByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<int> GetTotalAppointmentedByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );

    IQueryable<Appointment> GetAppointmentsQueryable(Guid userId);
}
