namespace Clinic.Application.Features.Admin.GetAllMedicine;

/// <summary>
///     Extension Method for GetAllMedicine features.
/// </summary>
public static class GetAllMedicineExtensionMethod
{
    public static string ToAppCode(this GetAllMedicineResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllMedicine)}Feature: {statusCode}";
    }
}
