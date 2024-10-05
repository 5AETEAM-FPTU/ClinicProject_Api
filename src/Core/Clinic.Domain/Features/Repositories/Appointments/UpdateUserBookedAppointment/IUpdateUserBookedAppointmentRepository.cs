using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Appointments.UpdateUserBookedAppointment;

public interface IUpdateUserBookedAppointmentRepository
{
    Task<Appointment> GetAppointmentByIdAsync(Guid id, CancellationToken ct);

    Task<bool> UpdateUserBookedAppointmentCommandAsync(
        Guid appointmentId,
        Guid userId,
        Guid slotId, CancellationToken ct
    );
}
