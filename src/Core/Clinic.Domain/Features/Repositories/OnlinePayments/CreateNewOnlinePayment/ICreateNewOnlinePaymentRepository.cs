using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.OnlinePayments.CreateNewOnlinePayment;

/// <summary>
/// Interface ICreateNewOnlinePaymentRepository
/// </summary>
///
public interface ICreateNewOnlinePaymentRepository { 
    Task<bool> IsUserHasCorrectlyAppointmentWithIdAsync(Guid userId, Guid appointmentId, CancellationToken cancellationToken = default);

    Task<bool> CreateNewOnlinePaymentAsync(OnlinePayment onlinePayment, CancellationToken cancellationToken = default);

    Task<bool> IsAppointmentHasDepositedAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
