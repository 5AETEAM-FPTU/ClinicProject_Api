namespace Clinic.WebAPI.EndPoints.ServiceOrders.AddOrderService.HttpResponseMapper;

/// <summary>
///     AddOrderService extension method
/// </summary>
internal static class AddOrderServiceHttpResponseMapper
{
    private static AddOrderServiceHttpResponseManager _manager;

    internal static AddOrderServiceHttpResponseManager Get()
    {
        return _manager ??= new();
    }
}
