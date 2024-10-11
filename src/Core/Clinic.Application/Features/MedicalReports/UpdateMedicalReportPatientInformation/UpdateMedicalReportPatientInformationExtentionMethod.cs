namespace Clinic.Application.Features.MedicalReports.UpdateMedicalReportPatientInformation;

public static class UpdateMedicalReportPatientInformationExtentionMethod
{
    public static string ToAppCode(
        this UpdateMedicalReportPatientInformationResponseStatusCode StatusCode
    )
    {
        return $"{nameof(UpdateMedicalReportPatientInformation)}Feature: {StatusCode}";
    }
}
