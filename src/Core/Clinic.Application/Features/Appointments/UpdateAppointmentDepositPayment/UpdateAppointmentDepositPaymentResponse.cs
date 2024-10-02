using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

public class UpdateAppoinmentDepositPaymentResponse: IFeatureResponse
{
    public UpdateAppointmentDepositPaymentResponseStatusCode StatusCode { get; init; }
}