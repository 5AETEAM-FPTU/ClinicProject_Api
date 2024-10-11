using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdatePatientInformation.HttpResponseMapper;

internal static class UpdatePatientInformationHttpResponseMapper
{
    private static UpdatePatientInformationHttpResponseManager _manager = new();

    internal static UpdatePatientInformationHttpResponseManager Get() => _manager ??= new();
}
