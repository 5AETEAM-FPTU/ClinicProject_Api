using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;

/// <summary>
///     GetServiceOrderItems Request
/// </summary>

public class GetServiceOrderItemsRequest : IFeatureRequest<GetServiceOrderItemsResponse>
{
    [BindFrom("serviceOrderId")]
    public Guid ServiceOrderId { get; set; }
}
