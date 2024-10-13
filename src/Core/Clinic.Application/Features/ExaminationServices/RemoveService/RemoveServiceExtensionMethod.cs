
namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     Extension Method for RemoveService features.
/// </summary>
public static class RemoveServiceExtensionMethod
{
    public static string ToAppCode(this RemoveServiceResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveService)}Feature: {statusCode}";
    }
}
