using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     UpdateMedicineOrderItem Request
/// </summary>

public class UpdateNoteMedicineOrderRequest : IFeatureRequest<UpdateNoteMedicineOrderResponse>
{
    [BindFrom("OrderId")]
    public Guid MedicineOrderId { get; init; }
    public string Note {  get; init; }

}
