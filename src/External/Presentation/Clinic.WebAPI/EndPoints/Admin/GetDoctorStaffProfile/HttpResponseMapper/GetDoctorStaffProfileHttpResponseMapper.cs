namespace Clinic.WebAPI.EndPoints.Admin.GetDoctorStaffProfile.HttpResponseMapper;

public static class GetDoctorStaffProfileHttpResponseMapper
{
    private static GetDoctorStaffProfileHttpResponseManager _manager;

    internal static GetDoctorStaffProfileHttpResponseManager Get()
    {
        return _manager ??= new GetDoctorStaffProfileHttpResponseManager();
    }
}
