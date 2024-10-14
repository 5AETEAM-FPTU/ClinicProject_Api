using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ServiceOrders.AddOrderService;

/// <summary>
///     AddOrderService Request
/// </summary>

public class AddOrderServiceRequest : IFeatureRequest<AddOrderServiceResponse>
{
    public Guid ServiceOrderId { get; init; }
    public IEnumerable<Guid> ServiceIds { get; init; }

}
