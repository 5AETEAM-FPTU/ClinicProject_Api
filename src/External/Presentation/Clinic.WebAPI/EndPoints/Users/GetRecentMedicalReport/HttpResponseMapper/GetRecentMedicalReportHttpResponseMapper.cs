namespace Clinic.WebAPI.EndPoints.Users.GetRecentMedicalReport.HttpResponseMapper;

/// <summary>
///     GetRecentMedicalReport extension method
/// </summary>
internal static class GetRecentMedicalReportHttpResponseMapper
{
    private static GetRecentMedicalReportHttpResponseManager _GetRecentMedicalReportHttpResponseManager;

    internal static GetRecentMedicalReportHttpResponseManager Get()
    {
        return _GetRecentMedicalReportHttpResponseManager ??= new();
    }
}
