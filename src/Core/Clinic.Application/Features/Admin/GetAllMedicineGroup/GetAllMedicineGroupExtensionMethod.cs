namespace Clinic.Application.Features.Admin.GetAllMedicineGroup;

/// <summary>
///     Extension Method for GetAllMedicineGroup features.
/// </summary>
public static class GetAllMedicineGroupExtensionMethod
{
    public static string ToAppCode(this GetAllMedicineGroupResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllMedicineGroup)}Feature: {statusCode}";
    }
}
