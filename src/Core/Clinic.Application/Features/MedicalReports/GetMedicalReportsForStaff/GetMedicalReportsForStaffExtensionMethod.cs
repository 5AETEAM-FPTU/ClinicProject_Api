namespace Clinic.Application.Features.MedicalReports.GetMedicalReportsForStaff;

/// <summary>
///     Extension Method for GetMedicalReportsForStaff features.
/// </summary>
public static class GetMedicalReportsForStaffExtensionMethod
{
    public static string ToAppCode(this GetMedicalReportsForStaffResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicalReportsForStaff)}Feature: {statusCode}";
    }
}
