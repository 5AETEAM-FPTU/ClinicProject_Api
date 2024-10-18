namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentMedicalReportByUserId.HttpResponseMapper;

/// <summary>
///     GetRecentMedicalReportByUserId extension method
/// </summary>
internal static class GetRecentMedicalReportByUserIdHttpResponseMapper
{
    private static GetRecentMedicalReportByUserIdHttpResponseManager _getRecentMedicalReportByUserIdHttpResponseManager;

    internal static GetRecentMedicalReportByUserIdHttpResponseManager Get()
    {
        return _getRecentMedicalReportByUserIdHttpResponseManager ??= new();
    }
}
