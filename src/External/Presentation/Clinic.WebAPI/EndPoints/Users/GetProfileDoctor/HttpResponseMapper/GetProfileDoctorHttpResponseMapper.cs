using Clinic.WebAPI.EndPoints.Users.GetProfileUser.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileDoctor.HttpResponseMapper;

internal static class GetProfileDoctorHttpResponseMapper
{
    private static GetProfileDoctorHttpResponseManager _GetProfileDoctorHttpResponseManager;

    internal static GetProfileDoctorHttpResponseManager Get()
    {
        return _GetProfileDoctorHttpResponseManager ??= new();
    }
}

