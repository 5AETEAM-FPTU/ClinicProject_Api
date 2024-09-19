namespace Clinic.Application.Features.Doctors.UpdateDoctorAchievement;

/// <summary>
///     Extension Method for UpdateUserById features.
/// </summary>
public static class UpdateDoctorAchievementExtensionMethod
{
    public static string ToAppCode(this UpdateDoctorAchievementByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateDoctorAchievement)}Feature: {statusCode}";
    }
}
