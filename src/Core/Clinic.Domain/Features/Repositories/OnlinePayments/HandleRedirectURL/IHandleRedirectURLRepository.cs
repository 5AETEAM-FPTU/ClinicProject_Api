using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.OnlinePayments.HandleRedirectURL;

/// <summary>
/// Interface IHandleRedirectURLRepository
/// </summary>
public interface IHandleRedirectURLRepository
{
    Task<OnlinePayment> FindPaymentByAppointmentIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    );

    public Task<Appointment> FindAppointmentByIdQueryAsync(
        Guid appointmentId,
        CancellationToken cancellationToken = default
    );

    Task<bool> UpdatePaymentCommandAsync(
        OnlinePayment existPayment,
        OnlinePayment onlinePayment,
        CancellationToken cancellationToken = default
    );
}
