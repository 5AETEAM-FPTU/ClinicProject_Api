using Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorScheduleByDate.HttpResponseMapper;

public static class GetDoctorScheduleByDateHttpResponseMapper
{
    private static GetDoctorScheduleByDateHttpResponseManager _httpResponseManager;

    internal static GetDoctorScheduleByDateHttpResponseManager Get()
    {
        return _httpResponseManager ?? new GetDoctorScheduleByDateHttpResponseManager();
    }
}
