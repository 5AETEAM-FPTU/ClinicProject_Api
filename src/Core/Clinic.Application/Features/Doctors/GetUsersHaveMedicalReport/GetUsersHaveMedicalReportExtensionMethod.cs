namespace Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     Extension Method for GetUsersHaveMedicalReport features.
/// </summary>
public static class GetUsersHaveMedicalReportExtensionMethod
{
    public static string ToAppCode(this GetUsersHaveMedicalReportResponseStatusCode statusCode)
    {
        return $"{nameof(GetUsersHaveMedicalReport)}Feature: {statusCode}";
    }
}
