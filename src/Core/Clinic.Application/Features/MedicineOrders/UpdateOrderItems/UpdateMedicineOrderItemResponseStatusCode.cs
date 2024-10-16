namespace Clinic.Application.Features.MedicineOrders.UpdateOrderItems;

/// <summary>
///     GetMedicineOrderItems ResponseStatusCode
/// </summary>
public enum UpdateMedicineOrderItemResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    MEDICINE_ORDER_NOT_FOUND,
    MEDICINE_ITEM_NOT_FOUND
}
