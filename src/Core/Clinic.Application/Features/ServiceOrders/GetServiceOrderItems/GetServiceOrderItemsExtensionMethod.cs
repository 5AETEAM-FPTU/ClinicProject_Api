
namespace Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;

/// <summary>
///     Extension Method for GetServiceOrderItems features.
/// </summary>
public static class GetServiceOrderItemsExtensionMethod
{
    public static string ToAppCode(this GetServiceOrderItemsResponseStatusCode statusCode)
    {
        return $"{nameof(GetServiceOrderItems)}Feature: {statusCode}";
    }
}
