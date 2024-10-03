namespace Clinic.Application.Features.Doctors.GetRecentBookedAppointments;

/// <summary>
///     Extension Method for GetAppointmentsByDate features.
/// </summary>
public static class GetRecentBookedAppointmentsExtensionMethod
{
    public static string ToAppCode(this GetRecentBookedAppointmentsResponseStatusCode statusCode)
    {
        return $"{nameof(GetRecentBookedAppointments)}Feature: {statusCode}";
    }
}
