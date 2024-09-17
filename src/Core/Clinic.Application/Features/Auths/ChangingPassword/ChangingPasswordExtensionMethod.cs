namespace Clinic.Application.Features.Auths.ChangingPassword;

/// <summary>
///     Extension Method for ChangingPassword features.
/// </summary>
public static class ChangingPasswordExtensionMethod
{
    public static string ToAppCode(this ChangingPasswordResponseStatusCode statusCode)
    {
        return $"{nameof(ChangingPassword)}Feature: {statusCode}";
    }
}
