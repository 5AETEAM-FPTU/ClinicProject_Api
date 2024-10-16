namespace Clinic.WebAPI.EndPoints.MedicineOrders.GetMedicineOrderItems.HttpResponseMapper;

/// <summary>
///     GetMedicineOrderItems extension method
/// </summary>
internal static class GetMedicineOrderItemsHttpResponseMapper
{
    private static GetMedicineOrderItemsHttpResponseManager _manager;

    internal static GetMedicineOrderItemsHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
