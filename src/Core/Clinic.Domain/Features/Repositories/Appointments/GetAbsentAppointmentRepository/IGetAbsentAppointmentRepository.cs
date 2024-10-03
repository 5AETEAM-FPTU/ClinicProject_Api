using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.GetAbsentAppointment;

/// <summary>
///     Interface IGetAbsentAppointmentRepository
/// </summary>
public interface IGetAbsentAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAbsentAppointmentByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
