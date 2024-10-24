namespace Clinic.WebAPI.EndPoints.MedicalReports.GetMedicalReportsForStaff.HttpResponseMapper;

/// <summary>
///     GetMedicalReportsForStaff extension method
/// </summary>
internal static class GetMedicalReportsForStaffHttpResponseMapper
{
    private static GetMedicalReportsForStaffHttpResponseManager _GetMedicalReportsForStaffHttpResponseManager;

    internal static GetMedicalReportsForStaffHttpResponseManager Get()
    {
        return _GetMedicalReportsForStaffHttpResponseManager ??= new();
    }
}
