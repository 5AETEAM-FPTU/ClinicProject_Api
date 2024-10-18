
namespace Clinic.Application.Features.MedicineOrders.RemoveOrderItems;

/// <summary>
///     Extension Method for RemoveMedicineOrderItem features.
/// </summary>
public static class RemoveMedicineOrderItemExtensionMethod
{
    public static string ToAppCode(this RemoveMedicineOrderItemResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveOrderItems)}Feature: {statusCode}";
    }
}
