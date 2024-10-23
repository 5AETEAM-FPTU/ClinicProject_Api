namespace Clinic.WebAPI.EndPoints.ServiceOrders.UpdateStatusItem.HttpResponseMapper;

/// <summary>
///     UpdateStatusServiceOrderItem http response manager
/// </summary>
internal static class UpdateStatusServiceOrderItemsHttpResponseMapper
{
    private static UpdateStatusServiceOrderItemsHttpResponseManager _manager;

    internal static UpdateStatusServiceOrderItemsHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
