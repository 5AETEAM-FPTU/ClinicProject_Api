namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     Extension Method for GetSchedulesByDate features.
/// </summary>
public static class GetSchedulesByDateExtensionMethod
{
    public static string ToAppCode(this GetSchedulesByDateResponseStatusCode statusCode)
    {
        return $"{nameof(GetSchedulesByDate)}Feature: {statusCode}";
    }
}
