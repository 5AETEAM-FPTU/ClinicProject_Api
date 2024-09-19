namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateUserPrivateInfoExtensionMethod
{
    public static string ToAppCode(this UpdateUserPrivateInfoResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateUserPrivateInfo)}Feature: {statusCode}";
    }
}
