namespace Clinic.Application.Features.Admin.UpdateMedicine;

/// <summary>
///     Extension Method for UpdateMedicine features.
/// </summary>
public static class UpdateMedicineExtensionMethod
{
    public static string ToAppCode(this UpdateMedicineResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateMedicine)}Feature: {statusCode}";
    }
}
