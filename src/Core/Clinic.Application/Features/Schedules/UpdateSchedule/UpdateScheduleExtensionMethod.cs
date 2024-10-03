namespace Clinic.Application.Features.Schedules.UpdateSchedule;

/// <summary>
///     Extension Method for CreateSchedules features.
/// </summary>
public static class UpdateScheduleExtensionMethod
{
    public static string ToAppCode(this UpdateScheduleResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateSchedule)}Feature: {statusCode}";
    }
}
