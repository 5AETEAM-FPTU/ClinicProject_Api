namespace Clinic.Application.Features.Appointments.GetAppointmentUpcoming;

/// <summary>
///     Extension Method for GetAppointmentUpcoming features.
/// </summary>
public static class GetAppointmentUpcomingExtensionMethod
{
    public static string ToAppCode(this GetAppointmentUpcomingResponseStatusCode statusCode)
    {
        return $"{nameof(GetAppointmentUpcoming)}Feature: {statusCode}";
    }
}
