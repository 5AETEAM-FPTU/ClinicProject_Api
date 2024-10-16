
namespace Clinic.Application.Features.Users.GetAllMedicalReports;

/// <summary>
///     Extension Method for GetAllMedicalReport features.
/// </summary>
public static class GetAllUserMedicalReportsExtensionMethod
{
    public static string ToAppCode(this GetAllUserMedicalReportsResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllMedicalReports)}Feature: {statusCode}";
    }
}