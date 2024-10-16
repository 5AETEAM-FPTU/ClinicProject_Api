namespace Clinic.Application.Features.MedicineOrders.RemoveOrderItems;

/// <summary>
///     RemoveMedicineOrderItem ResponseStatusCode
/// </summary>
public enum RemoveMedicineOrderItemResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    MEDICINE_ORDER_NOT_FOUND,
    MEDICINE_ITEM_NOT_FOUND,
    FORBIDDEN
}
