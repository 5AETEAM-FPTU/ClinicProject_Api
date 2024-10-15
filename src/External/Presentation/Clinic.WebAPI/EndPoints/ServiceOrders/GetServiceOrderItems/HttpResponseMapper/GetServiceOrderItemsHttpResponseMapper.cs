namespace Clinic.WebAPI.EndPoints.ServiceOrders.GetServiceOrderItems.HttpResponseMapper;

/// <summary>
///     GetServiceOrderItems extension method
/// </summary>
internal static class GetServiceOrderItemsHttpResponseMapper
{
    private static GetServiceOrderItemsHttpResponseManager _manager;

    internal static GetServiceOrderItemsHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
