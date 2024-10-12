
namespace Clinic.Application.Features.ExaminationServices.GetDetailService;

/// <summary>
///     Extension Method for GetMedicineById features.
/// </summary>
public static class GetDetailServiceExtensionMethod
{
    public static string ToAppCode(this GetDetailServiceResponseStatusCode statusCode)
    {
        return $"{nameof(GetDetailService)}Feature: {statusCode}";
    }
}
