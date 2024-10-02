namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

/// <summary>
///     Mapper for mapping <see cref="UpdateAppointmentDepositPaymentResponse" /> to <see cref="UpdateAppointmentDepositPaymentHttpResponse" />.
/// </summary>
internal static class UpdateAppointm1entDepositPaymentHttpResponseMapper
{
    private static UpdateAppointmentDepositPaymentHttpReponseManager _updateAppointm1entDepositPaymentHttpReponseManager;

    internal static UpdateAppointmentDepositPaymentHttpReponseManager Get()
    {
        return _updateAppointm1entDepositPaymentHttpReponseManager ??= new();
    }
}
