namespace Clinic.Application.Features.Users.UpdateDoctorDescription;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateDoctorDescriptionByIdExtensionMethod
{
    public static string ToAppCode(this UpdateDoctorDescriptionByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePrivateDoctorInfoById)}Feature: {statusCode}";
    }
}
