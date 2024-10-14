
namespace Clinic.Application.Features.ExaminationServices.GetAllServices;

/// <summary>
///     Extension Method for GetAllServices features.
/// </summary>
public static class GetAllServicesExtensionMethod
{
    public static string ToAppCode(this GetAllServicesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllServices)}Feature: {statusCode}";
    }
}
