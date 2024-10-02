namespace Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;

/// <summary>
///     Extension Method for GetScheduleDatesByMonth features.
/// </summary>
public static class GetScheduleDatesByMonthExtensionMethod
{
    public static string ToAppCode(this GetScheduleDatesByMonthResponseStatusCode statusCode)
    {
        return $"{nameof(GetScheduleDatesByMonth)}Feature: {statusCode}";
    }
}
