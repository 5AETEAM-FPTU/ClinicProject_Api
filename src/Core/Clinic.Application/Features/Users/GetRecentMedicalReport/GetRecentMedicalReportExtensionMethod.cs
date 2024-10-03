namespace Clinic.Application.Features.Users.GetRecentMedicalReport;

/// <summary>
///     Extension Method for GetRecentMedicalReport features.
/// </summary>
public static class GetRecentMedicalReportExtensionMethod
{
    public static string ToAppCode(this GetRecentMedicalReportResponseStatusCode statusCode)
    {
        return $"{nameof(GetRecentMedicalReport)}Feature: {statusCode}";
    }
}
