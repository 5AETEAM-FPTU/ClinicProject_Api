namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder.HttpResponseMapper;

/// <summary>
///     UpdateNoteMedicineOrder extension method
/// </summary>
internal static class UpdateNoteMedicineOrderHttpResponseMapper
{
    private static UpdateNoteMedicineOrderHttpResponseManager _manager;

    internal static UpdateNoteMedicineOrderHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
