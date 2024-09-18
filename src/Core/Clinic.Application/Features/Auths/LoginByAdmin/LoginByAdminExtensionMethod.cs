namespace Clinic.Application.Features.Auths.LoginByAdmin;

/// <summary>
///     Extension Method for LoginByAdmin features.
/// </summary>
public static class LoginByAdminExtensionMethod
{
    public static string ToAppCode(this LoginByAdminResponseStatusCode statusCode)
    {
        return $"{nameof(LoginByAdmin)}Feature: {statusCode}";
    }
}
