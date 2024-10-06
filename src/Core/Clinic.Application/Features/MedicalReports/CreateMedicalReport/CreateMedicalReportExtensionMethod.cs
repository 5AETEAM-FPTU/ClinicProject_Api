namespace Clinic.Application.Features.MedicalReports.CreateMedicalReport;

public static class CreateMedicalReportExtensionMethod
{
    public static string ToAppCode(this CreateMedicalReportResponseStatusCode statusCode)
    {
        return $"{nameof(CreateMedicalReport)}Feature: {statusCode}";
    }
}
