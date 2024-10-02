namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

public static class UpdateAppointmentDepositPaymentExtensionMethod {
    public static string ToAppCode(this UpdateAppointmentDepositPaymentResponseStatusCode statusCode) {
        return $"{nameof(UpdateAppointmentDepositPayment)}Feature: {statusCode}";
    }
}