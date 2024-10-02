using System;
using System.Threading;
using System.Threading.Tasks;

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
}
