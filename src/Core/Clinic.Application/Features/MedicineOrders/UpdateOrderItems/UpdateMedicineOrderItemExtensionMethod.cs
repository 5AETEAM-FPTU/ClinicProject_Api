
namespace Clinic.Application.Features.MedicineOrders.UpdateOrderItems;

/// <summary>
///     Extension Method for UpdateMedicineOrderItem features.
/// </summary>
public static class UpdateMedicineOrderItemExtensionMethod
{
    public static string ToAppCode(this UpdateMedicineOrderItemResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateOrderItems)}Feature: {statusCode}";
    }
}
