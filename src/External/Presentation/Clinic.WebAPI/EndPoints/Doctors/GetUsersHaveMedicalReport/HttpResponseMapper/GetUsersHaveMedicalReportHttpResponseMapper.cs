namespace Clinic.WebAPI.EndPoints.Doctors.GetUsersHaveMedicalReport.HttpResponseMapper;

internal static class GetUsersHaveMedicalReportHttpResponseMapper
{
    private static GetUsersHaveMedicalReportHttpResponseManager _getUsersHaveMedicalReportHttpResponseManager;

    internal static GetUsersHaveMedicalReportHttpResponseManager Get()
    {
        return _getUsersHaveMedicalReportHttpResponseManager ??= new();
    }
}
