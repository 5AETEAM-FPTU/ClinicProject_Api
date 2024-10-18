namespace Clinic.Application.Features.Doctors.GetUserNotification;

/// <summary>
///     Extension Method for GetUserNotification features.
/// </summary>
public static class GetUserNotificationExtensionMethod
{
    public static string ToAppCode(this GetUserNotificationResponseStatusCode statusCode)
    {
        return $"{nameof(GetUserNotification)}Feature: {statusCode}";
    }
}
