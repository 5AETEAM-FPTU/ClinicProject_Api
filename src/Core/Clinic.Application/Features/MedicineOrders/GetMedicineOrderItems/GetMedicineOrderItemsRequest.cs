using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.MedicineOrders.GetMedicineOrderItems;

/// <summary>
///     GetMedicineOrderItems Request
/// </summary>

public class GetMedicineOrderItemsRequest : IFeatureRequest<GetMedicineOrderItemsResponse>
{
    [BindFrom("medicineOrderId")]
    public Guid MedicineOrderId { get; init; }
}
