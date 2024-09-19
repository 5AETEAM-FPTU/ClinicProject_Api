namespace Clinic.WebAPI.EndPoints.Doctors.AddDoctor.HttpResponseMapper;

internal static class AddDoctorHttpResponseMapper
{
    private static AddDoctorHttpResponseManager updateDoctorAchievementHttpResponseManager;

    internal static AddDoctorHttpResponseManager Get()
    {
        return updateDoctorAchievementHttpResponseManager ??= new();
    }
}
