using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems Request
/// </summary>

public class UpdateStatusServiceOrderItemsRequest : IFeatureRequest<UpdateStatusServiceOrderItemsResponse>
{
    public Guid ServiceOrderId { get; init; }
    public Guid ServiceId { get; init; }
}
