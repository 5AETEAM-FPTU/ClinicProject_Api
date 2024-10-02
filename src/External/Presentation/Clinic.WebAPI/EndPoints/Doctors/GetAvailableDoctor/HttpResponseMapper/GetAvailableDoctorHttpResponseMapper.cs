namespace Clinic.WebAPI.EndPoints.Doctors.GetAvailableDoctor.HttpResponseMapper;

internal static class GetAvailableDoctorHttpResponseMapper
{
    private static GetAvailableDoctorHttpResponseManager _GetAvailableDoctorHttpResponseManager;

    internal static GetAvailableDoctorHttpResponseManager Get()
    {
        return _GetAvailableDoctorHttpResponseManager ??= new();
    }
}
