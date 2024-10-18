namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateOrderItems.HttpResponseMapper;

/// <summary>
///     UpdateMedicineOrderItem extension method
/// </summary>
internal static class UpdateMedicineOrderItemHttpResponseMapper
{
    private static UpdateMedicineOrderItemHttpResponseManager _manager;

    internal static UpdateMedicineOrderItemHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
