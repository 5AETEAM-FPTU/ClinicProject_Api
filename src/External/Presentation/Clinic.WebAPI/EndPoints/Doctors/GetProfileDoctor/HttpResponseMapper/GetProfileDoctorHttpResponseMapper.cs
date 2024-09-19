namespace Clinic.WebAPI.EndPoints.Doctors.GetProfileDoctor.HttpResponseMapper;

internal static class GetProfileDoctorHttpResponseMapper
{
    private static GetProfileDoctorHttpResponseManager _GetProfileDoctorHttpResponseManager;

    internal static GetProfileDoctorHttpResponseManager Get()
    {
        return _GetProfileDoctorHttpResponseManager ??= new();
    }
}
