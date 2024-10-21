namespace Clinic.WebAPI.EndPoints.Doctors.GetAllDoctorForStaff.HttpResponseMapper;

internal static class GetAllDoctorForStaffHttpResponseMapper
{
    private static GetAllDoctorForStaffHttpResponseManager _GetAllDoctorForStaffHttpResponseManager;

    internal static GetAllDoctorForStaffHttpResponseManager Get()
    {
        return _GetAllDoctorForStaffHttpResponseManager ??= new();
    }
}
