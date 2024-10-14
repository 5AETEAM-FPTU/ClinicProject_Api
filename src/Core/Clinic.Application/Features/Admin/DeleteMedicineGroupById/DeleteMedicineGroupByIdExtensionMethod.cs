namespace Clinic.Application.Features.Admin.DeleteMedicineGroupById;

/// <summary>
///     Extension Method for DeleteMedicineGroupById features.
/// </summary>
public static class DeleteMedicineGroupByIdExtensionMethod
{
    public static string ToAppCode(this DeleteMedicineGroupByIdResponseStatusCode statusCode)
    {
        return $"{nameof(DeleteMedicineGroupById)}Feature: {statusCode}";
    }
}
