namespace Clinic.Application.Features.Admin.GetMedicineById;

/// <summary>
///     Extension Method for GetMedicineById features.
/// </summary>
public static class GetMedicineByIdExtensionMethod
{
    public static string ToAppCode(this GetMedicineByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicineById)}Feature: {statusCode}";
    }
}
