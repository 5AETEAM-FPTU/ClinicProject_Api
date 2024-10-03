namespace Clinic.Application.Features.Doctors.GetMedicalReportById;

/// <summary>
///     Extension Method for GetMedicalReportById features.
/// </summary>
public static class GetMedicalReportByIdExtensionMethod
{
    public static string ToAppCode(this GetMedicalReportByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicalReportById)}Feature: {statusCode}";
    }
}