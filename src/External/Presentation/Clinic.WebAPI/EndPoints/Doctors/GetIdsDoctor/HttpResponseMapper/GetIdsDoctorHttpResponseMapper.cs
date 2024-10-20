namespace Clinic.WebAPI.EndPoints.Doctors.GetIdsDoctor.HttpResponseMapper;

/// <summary>
///     GetIdsDoctorHttpResponse Mapper
/// </summary>
internal static class GetIdsDoctorHttpResponseMapper
{
    private static GetIdsDoctorHttpResponseManager _GetIdsDoctorHttpResponseManager;

    internal static GetIdsDoctorHttpResponseManager Get()
    {
        return _GetIdsDoctorHttpResponseManager ??= new();
    }
}
