namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdatePrivateDoctorInfoByIdExtensionMethod
{
    public static string ToAppCode(this UpdatePrivateDoctorInfoByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdatePrivateDoctorInfo)}Feature: {statusCode}";
    }
}
