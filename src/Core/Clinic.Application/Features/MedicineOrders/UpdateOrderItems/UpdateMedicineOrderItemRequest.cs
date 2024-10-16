using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.UpdateOrderItems;

/// <summary>
///     UpdateMedicineOrderItem Request
/// </summary>

public class UpdateMedicineOrderItemRequest : IFeatureRequest<UpdateMedicineOrderItemResponse>
{
    public Guid MedicineOrderId { get; init; }
    public Guid MedicineId { get; init; }
    public int Quantity { get; init; }
    public string Description { get; init; }
}
