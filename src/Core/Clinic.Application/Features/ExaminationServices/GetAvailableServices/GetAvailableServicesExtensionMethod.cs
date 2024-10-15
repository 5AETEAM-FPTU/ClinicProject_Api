using Clinic.Application.Features.ExaminationServices.GetAllServices;

namespace Clinic.Application.Features.ExaminationServices.GetAvailableServices;

/// <summary>
///     Extension Method for GetAvailableServices features.
/// </summary>
public static class GetAvailableServicesExtensionMethod
{
    public static string ToAppCode(this GetAvailableServicesResponseStatusCode statusCode)
    {
        return $"{nameof(GetAvailableServices)}Feature: {statusCode}";
    }
}
