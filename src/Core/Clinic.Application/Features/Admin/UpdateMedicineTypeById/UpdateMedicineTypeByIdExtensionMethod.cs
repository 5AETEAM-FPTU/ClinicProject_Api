namespace Clinic.Application.Features.Admin.UpdateMedicineTypeById;

/// <summary>
///     Extension Method for UpdateMedicineTypeById features.
/// </summary>
public static class UpdateMedicineTypeByIdExtensionMethod
{
    public static string ToAppCode(this UpdateMedicineTypeByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateMedicineTypeById)}Feature: {statusCode}";
    }
}
