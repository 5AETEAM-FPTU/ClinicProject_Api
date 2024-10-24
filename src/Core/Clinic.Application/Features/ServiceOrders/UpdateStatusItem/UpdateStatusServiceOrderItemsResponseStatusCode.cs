namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems Response Status Code
/// </summary>
public enum UpdateStatusServiceOrderItemsResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    SERVICE_ITEM_NOT_FOUND,
    FORBIDDEN
}
