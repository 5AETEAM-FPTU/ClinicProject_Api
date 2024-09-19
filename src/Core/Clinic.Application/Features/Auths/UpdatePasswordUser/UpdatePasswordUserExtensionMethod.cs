namespace Clinic.Application.Features.Auths.UpdatePasswordUser;

/// <summary>
///     Extension Method for UpdatePasswordUser features.
/// </summary>
public static class UpdatePasswordUserExtensionMethod
{
    public static string ToAppCode(this UpdatePasswordUserResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePasswordUser)}Feature: {statusCode}";
    }
}
