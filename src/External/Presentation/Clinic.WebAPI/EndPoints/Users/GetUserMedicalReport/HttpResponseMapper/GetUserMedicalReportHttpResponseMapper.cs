namespace Clinic.WebAPI.EndPoints.Users.GetUserMedicalReport.HttpResponseMapper;

internal static class GetUserMedicalReportHttpResponseMapper
{
    private static GetUserMedicalReportHttpResponseManager _GetUserMedicalReportHttpResponseManager;

    internal static GetUserMedicalReportHttpResponseManager Get()
    {
        return _GetUserMedicalReportHttpResponseManager ??= new();
    }
}
