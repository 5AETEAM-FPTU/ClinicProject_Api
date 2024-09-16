namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     Extension Method for ForgotPassword features.
/// </summary>
public static class ForgotPasswordExtensionMethod
{
    public static string ToAppCode(this ForgotPasswordResponseStatusCode statusCode)
    {
        return $"{nameof(ForgotPassword)}Feature: {statusCode}";
    }
}
