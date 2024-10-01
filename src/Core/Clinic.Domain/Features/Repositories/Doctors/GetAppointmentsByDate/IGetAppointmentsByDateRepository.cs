using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Doctors.GetAppointmentsByDate;

/// <summary>
///     Interface for Query GetAppointmentsByDate Repository
/// </summary>
public interface IGetAppointmentsByDateRepository
{
    Task<IEnumerable<Appointment>> GetAppointmentsByDateQueryAsync(
        DateTime startDate,
        DateTime? endDate,
        Guid userId,
        CancellationToken cancellationToken = default
    );

    public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}
