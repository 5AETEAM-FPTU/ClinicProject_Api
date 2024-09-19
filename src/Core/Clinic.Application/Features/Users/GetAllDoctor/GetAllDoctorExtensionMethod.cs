
namespace Clinic.Application.Features.Users.GetAllDoctor;

/// <summary>
///     Extension Method for GetAllDoctors features.
/// </summary>
public static class GetAllDoctorExtensionMethod
{
    public static string ToAppCode(this GetAllDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllDoctor)}Feature: {statusCode}";
    }
}