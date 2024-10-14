
namespace Clinic.Application.Features.ServiceOrders.AddOrderService;

/// <summary>
///     Extension Method for AddOrderService features.
/// </summary>
public static class AddOrderServiceExtensionMethod
{
    public static string ToAppCode(this AddOrderServiceResponseStatusCode statusCode)
    {
        return $"{nameof(AddOrderService)}Feature: {statusCode}";
    }
}
