namespace Clinic.WebAPI.EndPoints.MedicineOrders.OrderMedicines.HttpResponseMapper;

/// <summary>
///     GetMedicineOrderItems extension method
/// </summary>
internal static class OrderMedicinesHttpResponseMapper
{
    private static OrderMedicinesHttpResponseManager _manager;

    internal static OrderMedicinesHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
