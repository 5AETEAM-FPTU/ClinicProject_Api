namespace Clinic.Application.Features.Appointments.GetAbsentAppointment;

/// <summary>
///     Extension Method for GetAbsentAppointment features.
/// </summary>
public static class GetAbsentAppointmentExtensionMethod
{
    public static string ToAppCode(this GetAbsentAppointmentResponseStatusCode statusCode)
    {
        return $"{nameof(GetAbsentAppointment)}Feature: {statusCode}";
    }
}
