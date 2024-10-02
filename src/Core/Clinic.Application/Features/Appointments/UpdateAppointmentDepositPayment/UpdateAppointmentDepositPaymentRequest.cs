using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

public class UpdateAppoinmentDepositPaymenRequest
    : IFeatureRequest<UpdateAppoinmentDepositPaymentResponse>
{
    public Guid AppointmentId { get; init; }

    public bool IsDepositPayment { get; init; }
}
