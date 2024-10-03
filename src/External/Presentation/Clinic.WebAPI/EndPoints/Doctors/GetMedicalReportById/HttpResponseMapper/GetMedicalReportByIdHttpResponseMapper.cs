namespace Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById.HttpResponseMapper;

internal static class GetMedicalReportByIdHttpResponseMapper
{
    private static GetMedicalReportByIdHttpResponseManager _GetMedicalReportByIdHttpResponseManager;

    internal static GetMedicalReportByIdHttpResponseManager Get()
    {
        return _GetMedicalReportByIdHttpResponseManager ??= new();
    }
}
