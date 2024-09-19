namespace Clinic.WebAPI.EndPoints.Users.UpdatePrivateDoctorInfo.HttpResponseMapper;

internal static class UpdatePrivateDoctorInfoHttpResponseMapper
{
    private static UpdatePrivateDoctorInfoHttpResponseManager _UpdatePrivateDoctorInfoHttpResponseManager;

    internal static UpdatePrivateDoctorInfoHttpResponseManager Get()
    {
        return _UpdatePrivateDoctorInfoHttpResponseManager ??= new();
    }
}
