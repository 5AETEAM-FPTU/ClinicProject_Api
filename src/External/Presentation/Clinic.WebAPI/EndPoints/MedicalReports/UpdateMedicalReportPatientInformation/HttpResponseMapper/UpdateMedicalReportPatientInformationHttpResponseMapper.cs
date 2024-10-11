using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdateMedicalReportPatientInformation.HttpResponseMapper;

internal static class UpdateMedicalReportPatientInformationHttpResponseMapper
{
    private static UpdateMedicalReportPatientInformationHttpResponseManager _manager = new();

    internal static UpdateMedicalReportPatientInformationHttpResponseManager Get() =>
        _manager ??= new();
}
