using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.MedicineOrders.RemoveOrderItems;

/// <summary>
///     RemoveMedicineOrderItem Request
/// </summary>

public class RemoveMedicineOrderItemRequest : IFeatureRequest<RemoveMedicineOrderItemResponse>
{
    [BindFrom("medicineOrderId")]
    public Guid MedicineOrderId { get; init; }

    [BindFrom("medicineId")]
    public Guid MedicineId { get; init; }
}
