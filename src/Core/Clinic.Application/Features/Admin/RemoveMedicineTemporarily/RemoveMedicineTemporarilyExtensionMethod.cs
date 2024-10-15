namespace Clinic.Application.Features.Admin.RemoveMedicineTemporarily;

/// <summary>
///     Extension Method for RemoveMedicineTemporarily features.
/// </summary>
public static class RemoveMedicineTemporarilyExtensionMethod
{
    public static string ToAppCode(this RemoveMedicineTemporarilyResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveMedicineTemporarily)}Feature: {statusCode}";
    }
}
