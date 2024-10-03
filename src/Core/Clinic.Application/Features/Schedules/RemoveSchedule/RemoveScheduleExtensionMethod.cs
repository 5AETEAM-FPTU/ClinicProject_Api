namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     Extension Method for CreateSchedules features.
/// </summary>
public static class RemoveScheduleExtensionMethod
{
    public static string ToAppCode(this RemoveScheduleResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveSchedule)}Feature: {statusCode}";
    }
}
