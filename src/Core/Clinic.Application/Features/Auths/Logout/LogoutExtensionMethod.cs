namespace Clinic.Application.Features.Auths.Logout;

/// <summary>
///     Extension Method for Logout features.
/// </summary>
public static class LogoutExtensionMethod
{
    public static string ToAppCode(this LogoutResponseStatusCode statusCode)
    {
        return $"{nameof(Logout)}Feature: {statusCode}";
    }
}
