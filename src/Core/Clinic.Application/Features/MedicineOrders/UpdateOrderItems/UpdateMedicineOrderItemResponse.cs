using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.UpdateOrderItems;

/// <summary>
///     UpdateMedicineOrderItem Response
/// </summary>
public class UpdateMedicineOrderItemResponse : IFeatureResponse
{
    public UpdateMedicineOrderItemResponseStatusCode StatusCode { get; init; }
}
