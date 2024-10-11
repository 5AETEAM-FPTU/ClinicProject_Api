using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;

public class UpdateMedicalReportPatientInformationResponse : IFeatureResponse
{
    public UpdateMedicalReportPatientInformationResponseStatusCode StatusCode { get; set; }
}
