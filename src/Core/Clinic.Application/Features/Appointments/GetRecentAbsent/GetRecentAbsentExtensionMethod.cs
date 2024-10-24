namespace Clinic.Application.Features.Appointments.GetRecentAbsent;

/// <summary>
///     Extension Method for GetRecentAbsent features.
/// </summary>
public static class GetRecentAbsentExtensionMethod
{
    public static string ToAppCode(this GetRecentAbsentResponseStatusCode statusCode)
    {
        return $"{nameof(GetRecentAbsent)}Feature: {statusCode}";
    }
}
