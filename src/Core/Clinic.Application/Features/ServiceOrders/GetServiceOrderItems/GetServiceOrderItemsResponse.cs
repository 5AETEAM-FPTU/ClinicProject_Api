using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.ServiceOrders.GetServiceOrderItems;

/// <summary>
///     GetServiceOrderItems Response Status Code
/// </summary>
public class GetServiceOrderItemsResponse : IFeatureResponse
{
    public GetServiceOrderItemsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public ServiceOrderDetail ServiceOrder {  get; init; }
        public sealed class ServiceOrderDetail
        {
            public Guid Id { get; init; }
            public int Quantity { get; init; }
            public int TotalPrice { get; init; }
            public bool IsAllUpdated { get; set; }

            public IEnumerable<ServiceOrderItem> Items { get; init; }

            public sealed class ServiceOrderItem
            {
                public bool IsUpdated { get; init; }                // update result or not
                public int PriceAtOrder { get; init; }
                public ServiceDetail Service { get; init; }
                public sealed class ServiceDetail
                {
                    public Guid Id { get; init; }
                    public string Code { get; init; }
                    public string Name { get; init; }
                    public string Descripiton { get; init; }
                    public string Group { get; init; }
                }
            }
        }
    }
}
