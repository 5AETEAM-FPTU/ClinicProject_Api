namespace Clinic.Application.Features.Appointments.GetRecentPending;

/// <summary>
///     Extension Method for GetRecentPending features.
/// </summary>
public static class GetRecentPendingExtensionMethod
{
    public static string ToAppCode(this GetRecentPendingResponseStatusCode statusCode)
    {
        return $"{nameof(GetRecentPending)}Feature: {statusCode}";
    }
}
