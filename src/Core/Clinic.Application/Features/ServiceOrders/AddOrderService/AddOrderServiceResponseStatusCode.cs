namespace Clinic.Application.Features.ServiceOrders.AddOrderService;

/// <summary>
///     AddOrderService Response Status Code
/// </summary>
public enum AddOrderServiceResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    FORBIDDEN,
    SERVICE_ORDER_NOT_FOUND,
    SERVICE_NOT_AVAILABLE,
}
