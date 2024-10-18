using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;

namespace Clinic.Application.Features.ServiceOrders.AddOrderService;

/// <summary>
///     AddOrderService Response
/// </summary>
public class AddOrderServiceResponse : IFeatureResponse
{
    public AddOrderServiceResponseStatusCode StatusCode { get; init; }

}
