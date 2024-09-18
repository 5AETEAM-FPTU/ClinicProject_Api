namespace Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdatePrivateDoctorInfoByIdExtensionMethod
{
    public static string ToAppCode(this UpdatePrivateDoctorInfoByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePrivateDoctorInfoById)}Feature: {statusCode}";
    }
}
