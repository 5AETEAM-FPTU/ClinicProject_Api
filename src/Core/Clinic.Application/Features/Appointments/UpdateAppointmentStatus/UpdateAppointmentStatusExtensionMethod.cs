namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;

/// <summary>
/// UpdateAppointmentStatusExtensionMethod
/// </summary>
public static class UpdateAppointmentStatusExtensionMethod
{
    public static string ToAppCode(this UpdateAppointmentStatusResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateAppointmentStatus)}Feature: {statusCode}";
    }
}
