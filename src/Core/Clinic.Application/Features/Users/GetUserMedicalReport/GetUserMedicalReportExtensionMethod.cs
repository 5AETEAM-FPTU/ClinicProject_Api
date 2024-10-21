namespace Clinic.Application.Features.Users.GetUserMedicalReport;

/// <summary>
///     Extension Method for GetUserMedicalReport features.
/// </summary>
public static class GetUserMedicalReportExtensionMethod
{
    public static string ToAppCode(this GetUserMedicalReportResponseStatusCode statusCode)
    {
        return $"{nameof(GetUserMedicalReport)}Feature: {statusCode}";
    }
}
