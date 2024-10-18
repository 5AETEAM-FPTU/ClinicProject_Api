namespace Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     GetMedicineOrderItems ResponseStatusCode
/// </summary>
public enum UpdateNoteMedicineOrderResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    MEDICINE_ORDER_NOT_FOUND,
    FORBIDDEN
}
