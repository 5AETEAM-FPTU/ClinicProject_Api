namespace Clinic.WebAPI.EndPoints.MedicineOrders.RemoveOrderItems.HttpResponseMapper;

/// <summary>
///     RemoveMedicineOrderItem extension method
/// </summary>
internal static class RemoveMedicineOrderItemHttpResponseMapper
{
    private static RemoveMedicineOrderItemHttpResponseManager _manager;

    internal static RemoveMedicineOrderItemHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
