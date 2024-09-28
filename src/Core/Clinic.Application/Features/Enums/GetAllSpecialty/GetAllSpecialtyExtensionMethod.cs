
namespace Clinic.Application.Features.Enums.GetAllSpecialty;

/// <summary>
///     Extension Method for GetAllSpecialty features.
/// </summary>
public static class GetAllSpecialtyExtensionMethod
{
    public static string ToAppCode(this GetAllSpecialtyResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllSpecialty)}Feature: {statusCode}";
    }
}
