using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems Response
/// </summary>
public class UpdateStatusServiceOrderItemsResponse : IFeatureResponse
{
    public UpdateStatusServiceOrderItemsResponseStatusCode StatusCode { get; init; }

}
