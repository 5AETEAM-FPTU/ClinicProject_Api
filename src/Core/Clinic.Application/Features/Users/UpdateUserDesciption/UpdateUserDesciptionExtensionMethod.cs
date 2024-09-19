namespace Clinic.Application.Features.Users.UpdateUserDesciption;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateUserDesciptionExtensionMethod
{
    public static string ToAppCode(this UpdateUserDesciptionResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateUserAvatar)}Feature: {statusCode}";
    }
}
