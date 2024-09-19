namespace Clinic.Application.Features.Doctors.UpdateDoctorDescription;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateDoctorDescriptionByIdExtensionMethod
{
    public static string ToAppCode(this UpdateDoctorDescriptionByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateDoctorDescription)}Feature: {statusCode}";
    }
}
