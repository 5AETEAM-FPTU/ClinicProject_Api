namespace Clinic.Application.Features.Schedules.CreateSchedules;

/// <summary>
///     Extension Method for CreateSchedules features.
/// </summary>
public static class CreateSchedulesExtensionMethod
{
    public static string ToAppCode(this CreateSchedulesResponseStatusCode statusCode)
    {
        return $"{nameof(CreateSchedules)}Feature: {statusCode}";
    }
}
