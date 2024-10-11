using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdateMainMedicalReportInformation.HttpResponseMapper;

public class UpdateMainMedicalReportInformationHttpResponseMapper
{
    private static UpdateMainMedicalReportInformationHttpResponseManager _manager = new();

    internal static UpdateMainMedicalReportInformationHttpResponseManager Get() =>
        _manager ??= new();
}
