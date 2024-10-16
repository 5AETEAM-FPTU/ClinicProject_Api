namespace Clinic.Application.Features.MedicineOrders.GetMedicineOrderItems;

/// <summary>
///     Extension Method for GetMedicineOrderItems features.
/// </summary>
public static class GetSMedicineOrderItemsExtensionMethod
{
    public static string ToAppCode(this GetMedicineOrderItemsResponseStatusCode statusCode)
    {
        return $"{nameof(GetMedicineOrderItems)}Feature: {statusCode}";
    }
}
