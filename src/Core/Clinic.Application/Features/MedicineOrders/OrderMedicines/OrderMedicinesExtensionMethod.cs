namespace Clinic.Application.Features.MedicineOrders.OrderMedicines;

/// <summary>
///     Extension Method for GetMedicineOrderItems features.
/// </summary>
public static class OrderMedicinesExtensionMethod
{
    public static string ToAppCode(this OrderMedicinesResponseStatusCode statusCode)
    {
        return $"{nameof(OrderMedicines)}Feature: {statusCode}";
    }
}
