using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace  Clinic.Domain.Features.Repositories.Appointments.UpdateAppointmentDepositPayment;

public interface IUpdateAppointmentDepositPaymentRepository
{
    Task<Appointment> GetAppointmentByIdAsync(Guid id, CancellationToken ct);

    Task<bool> UpdateAppointmentDepositPaymentCommandAsync(
        Guid appointmentId,
        bool depositPayment, CancellationToken ct
    );
}