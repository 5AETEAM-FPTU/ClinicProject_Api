

namespace Clinic.WebAPI.EndPoints.Users.UpdateDoctorAchievement.HttpResponseMapper;

internal static class UpdateDoctorAchievementHttpResponseMapper
{
    private static UpdateDoctorAchievementHttpResponseManager updateDoctorAchievementHttpResponseManager;

    internal static UpdateDoctorAchievementHttpResponseManager Get()
    {
        return updateDoctorAchievementHttpResponseManager ??= new();
    }
}
