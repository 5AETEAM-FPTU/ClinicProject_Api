namespace Clinic.Application.Features.Schedules.RemoveAllSchedules;

/// <summary>
///     Extension Method for CreateSchedules features.
/// </summary>
public static class RemoveAllSchedulesExtensionMethod
{
    public static string ToAppCode(this RemoveAllSchedulesResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveAllSchedules)}Feature: {statusCode}";
    }
}
