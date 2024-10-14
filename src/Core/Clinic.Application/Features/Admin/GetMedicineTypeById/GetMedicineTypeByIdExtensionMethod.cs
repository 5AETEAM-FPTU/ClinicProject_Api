namespace Clinic.Application.Features.Admin.GetMedicineTypeById;

/// <summary>
///     Extension Method for GetMedicineTypeById features.
/// </summary>
public static class GetMedicineTypeByIdExtensionMethod
{
    public static string ToAppCode(this GetMedicineTypeByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicineTypeById)}Feature: {statusCode}";
    }
}
