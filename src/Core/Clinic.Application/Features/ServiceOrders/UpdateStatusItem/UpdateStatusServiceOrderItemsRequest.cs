using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ServiceOrders.UpdateStatusItem;

/// <summary>
///     UpdateStatusServiceOrderItems Request
/// </summary>

public class UpdateStatusServiceOrderItemsRequest
    : IFeatureRequest<UpdateStatusServiceOrderItemsResponse>
{
    [BindFrom("serviceOrderId")]
    public Guid ServiceOrderId { get; init; }

    [BindFrom("serviceId")]
    public Guid ServiceId { get; init; }
}
