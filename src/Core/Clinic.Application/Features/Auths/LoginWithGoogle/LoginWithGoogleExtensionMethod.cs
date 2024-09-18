namespace Clinic.Application.Features.Auths.LoginWithGoogle;

/// <summary>
///     Extension Method for LoginWithGoogle features.
/// </summary>
public static class LoginWithGoogleExtensionMethod
{
    public static string ToAppCode(this LoginWithGoogleResponseStatusCode statusCode)
    {
        return $"{nameof(LoginWithGoogle)}Feature: {statusCode}";
    }
}
