namespace Clinic.Application.Features.Admin.GetMedicineGroupById;

/// <summary>
///     Extension Method for GetMedicineGroupById features.
/// </summary>
public static class GetMedicineGroupByIdExtensionMethod
{
    public static string ToAppCode(this GetMedicineGroupByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicineGroupById)}Feature: {statusCode}";
    }
}
