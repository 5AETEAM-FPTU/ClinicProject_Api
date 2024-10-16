namespace Clinic.Application.Features.MedicineOrders.OrderMedicines;

/// <summary>
///     GetMedicineOrderItems ResponseStatusCode
/// </summary>
public enum OrderMedicinesResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    MEDICINE_ORDER_NOT_FOUND,
    MEDICINE_NOT_AVAILABLE,
    MEDICINE_ALREADY_EXIST,
    FORBIDDEN
}
