namespace Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

/// <summary>
///     Extension Method for GetRecentMedicalReportByUserId features.
/// </summary>
public static class GetRecentMedicalReportByUserIdExtensionMethod
{
    public static string ToAppCode(this GetRecentMedicalReportByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetRecentMedicalReportByUserId)}Feature: {statusCode}";
    }
}
