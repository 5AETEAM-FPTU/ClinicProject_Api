namespace Clinic.Application.Features.Users.UpdateUserAvatar;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateUserAvatarExtensionMethod
{
    public static string ToAppCode(this UpdateUserAvatarResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateUserAvatar)}Feature: {statusCode}";
    }
}
