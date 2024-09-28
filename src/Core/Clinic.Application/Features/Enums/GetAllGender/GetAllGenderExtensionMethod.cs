
namespace Clinic.Application.Features.Enums.GetAllGender;

/// <summary>
///     Extension Method for GetAllGender features.
/// </summary>
public static class GetAllGenderExtensionMethod
{
    public static string ToAppCode(this GetAllGenderResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllGender)}Feature: {statusCode}";
    }
}