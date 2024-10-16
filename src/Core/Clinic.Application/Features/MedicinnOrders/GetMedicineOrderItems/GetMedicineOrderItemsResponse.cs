using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicinnOrders.GetMedicineOrderItems;

/// <summary>
///     GetMedicineOrderItems Response
/// </summary>
public class GetMedicineOrderItemsResponse : IFeatureResponse
{
    public GetMedicineOrderItemsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public MedicineOrderDetail MedicineOrder { get; init; }
        public sealed class MedicineOrderDetail
        {
            public Guid Id { get; init; }
            public int TotalItem { get; init; }
            public IEnumerable<MedicineOrderItem> Items { get; init; }

            public sealed class MedicineOrderItem
            {
                public int Quantity { get; init; }
                public string Description { get; init; }
                public ServiceDetail Service { get; init; }
                public sealed class ServiceDetail
                {
                    public Guid Id { get; init; }
                    public string Name { get; init; }
                    public MedicineTypeDetail Type { get; init; }

                    public sealed class MedicineTypeDetail
                    {
                        public Guid Id { get; init; }
                        public string Name { get; init; }  
                        public string Constant {  get; init; }
                    }
                }
            }
        }
    }
}
