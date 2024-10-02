namespace Clinic.Application.Features.Doctors.GetAllMedicalReport;

/// <summary>
///     Extension Method for GetAllMedicalReport features.
/// </summary>
public static class GetAllMedicalReportExtensionMethod
{
    public static string ToAppCode(this GetAllMedicalReportResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllMedicalReport)}Feature: {statusCode}";
    }
}