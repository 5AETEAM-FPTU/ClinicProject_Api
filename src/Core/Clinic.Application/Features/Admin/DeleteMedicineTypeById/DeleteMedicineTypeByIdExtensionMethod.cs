namespace Clinic.Application.Features.Admin.DeleteMedicineTypeById;

/// <summary>
///     Extension Method for DeleteMedicineTypeById features.
/// </summary>
public static class DeleteMedicineTypeByIdExtensionMethod
{
    public static string ToAppCode(this DeleteMedicineTypeByIdResponseStatusCode statusCode)
    {
        return $"{nameof(DeleteMedicineTypeById)}Feature: {statusCode}";
    }
}
