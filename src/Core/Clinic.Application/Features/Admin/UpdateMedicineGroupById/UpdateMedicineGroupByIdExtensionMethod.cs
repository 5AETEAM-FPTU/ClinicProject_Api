namespace Clinic.Application.Features.Admin.UpdateMedicineGroupById;

/// <summary>
///     Extension Method for UpdateMedicineGroupById features.
/// </summary>
public static class UpdateMedicineGroupByIdExtensionMethod
{
    public static string ToAppCode(this UpdateMedicineGroupByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateMedicineGroupById)}Feature: {statusCode}";
    }
}
