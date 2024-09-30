using Clinic.Domain.Commons.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace Clinic.Domain.Features.Repositories.Appointments.GetUserBookedAppointment;

public interface IGetUserBookedAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetUserBookedAppointmentByUserIdQueryAsync(Guid userId, CancellationToken cancellationToken);
}
