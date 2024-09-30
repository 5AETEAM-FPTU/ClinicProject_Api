namespace Clinic.Application.Features.Appointments.GetUserBookedAppointment;

/// <summary>
///     Extension Method for GetUserBookedAppointment features.
/// </summary>
public static class GetUserBookedAppointmentExtensionMethod
{
    public static string ToAppCode(this GetUserBookedAppointmentResponseStatusCode statusCode)
    {
        return $"{nameof(GetUserBookedAppointment)}Feature: {statusCode}";
    }
}
