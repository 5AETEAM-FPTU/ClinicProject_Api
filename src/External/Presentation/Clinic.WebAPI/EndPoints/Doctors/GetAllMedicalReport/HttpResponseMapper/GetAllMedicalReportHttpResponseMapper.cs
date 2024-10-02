namespace Clinic.WebAPI.EndPoints.Doctors.GetAllMedicalReport.HttpResponseMapper;

/// <summary>
///     GetAllMedicalReport extension method
/// </summary>
internal static class GetAllMedicalReportHttpResponseMapper
{
    private static GetAllMedicalReportHttpResponseManager _GetAllMedicalReportHttpResponseManager;

    internal static GetAllMedicalReportHttpResponseManager Get()
    {
        return _GetAllMedicalReportHttpResponseManager ??= new();
    }
}

