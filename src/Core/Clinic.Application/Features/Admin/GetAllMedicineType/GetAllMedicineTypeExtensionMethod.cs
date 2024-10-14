namespace Clinic.Application.Features.Admin.GetAllMedicineType;

/// <summary>
///     Extension Method for GetAllMedicineType features.
/// </summary>
public static class GetAllMedicineTypeExtensionMethod
{
    public static string ToAppCode(this GetAllMedicineTypeResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllMedicineType)}Feature: {statusCode}";
    }
}
