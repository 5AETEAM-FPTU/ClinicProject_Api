using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Appointments.UpdateAppointmentStatus;

public interface IUpdateAppointmentStatusRepository
{
    Task<bool> IsAppointmentExistedByIdAsync(Guid appointmentId, CancellationToken ct);
    Task<bool> IsStatusExistedByIdAsync(Guid statusId, CancellationToken ct);
    Task<bool> UpdateAppointmentStatusCommandAsync(Guid AppointmentId, Guid AppointmentStatusId, CancellationToken ct);
}
