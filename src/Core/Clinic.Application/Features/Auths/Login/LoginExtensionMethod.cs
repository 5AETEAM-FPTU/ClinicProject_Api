namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Extension Method for Login features.
/// </summary>
public static class LoginExtensionMethod
{
    public static string ToAppCode(this LoginResponseStatusCode statusCode)
    {
        return $"{nameof(Login)}Feature: {statusCode}";
    }
}
