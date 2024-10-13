namespace Clinic.Application.Features.Admin.DeleteMedicineById;

/// <summary>
///     Extension Method for DeleteMedicineById features.
/// </summary>
public static class DeleteMedicineByIdExtensionMethod
{
    public static string ToAppCode(this DeleteMedicineByIdResponseStatusCode statusCode)
    {
        return $"{nameof(DeleteMedicineById)}Feature: {statusCode}";
    }
}
