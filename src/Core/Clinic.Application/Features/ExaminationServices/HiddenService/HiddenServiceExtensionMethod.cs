using Clinic.Application.Features.ExaminationServices.RemoveService;

namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     Extension Method for HiddenService features.
/// </summary>
public static class HiddenServiceExtensionMethod
{
    public static string ToAppCode(this HiddenServiceResponseStatusCode statusCode)
    {
        return $"{nameof(HiddenService)}Feature: {statusCode}";
    }
}
