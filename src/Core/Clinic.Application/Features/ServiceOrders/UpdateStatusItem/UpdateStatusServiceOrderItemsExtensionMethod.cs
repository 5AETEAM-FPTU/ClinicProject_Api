
namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     Extension Method for UpdateStatusServiceOrderItems features.
/// </summary>
public static class UpdateStatusServiceOrderItemsExtensionMethod
{
    public static string ToAppCode(this UpdateStatusServiceOrderItemsResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateStatusItem)}Feature: {statusCode}";
    }
}
