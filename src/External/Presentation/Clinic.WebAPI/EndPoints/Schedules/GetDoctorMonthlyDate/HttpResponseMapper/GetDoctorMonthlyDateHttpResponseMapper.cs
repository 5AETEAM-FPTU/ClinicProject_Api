namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;

internal static class GetDoctorMonthlyDateHttpResponseMapper
{
    private static GetDoctorMonthlyDateHttpResponseManager _httpResponseManager;

    internal static GetDoctorMonthlyDateHttpResponseManager Get()
    {
        return _httpResponseManager ?? new GetDoctorMonthlyDateHttpResponseManager();
    }
}
