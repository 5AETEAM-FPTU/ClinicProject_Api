namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     Extension Method for GetSchedulesByDate features.
/// </summary>
public static class GetAppointmentsByDateExtensionMethod
{
    public static string ToAppCode(this GetAppointmentsByDateResponseStatusCode statusCode)
    {
        return $"{nameof(GetAppointmentsByDate)}Feature: {statusCode}";
    }
}
