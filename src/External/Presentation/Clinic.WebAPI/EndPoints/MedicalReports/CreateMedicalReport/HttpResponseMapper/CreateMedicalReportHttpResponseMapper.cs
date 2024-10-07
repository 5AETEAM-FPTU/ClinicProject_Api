namespace Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

/// <summary>
///     Mapper for <see cref="CreateMedicalReportResponse"/>
/// </summary>
internal static class CreateMedicalReportHttpResponseMapper
{
    private static CreateMedicalReportHttpResponseManager _manager = new();

    internal static CreateMedicalReportHttpResponseManager Get() => _manager ??= new();
}
