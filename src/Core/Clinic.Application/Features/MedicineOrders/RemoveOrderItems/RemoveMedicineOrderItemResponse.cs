using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.RemoveOrderItems;

/// <summary>
///     RemoveMedicineOrderItem Response
/// </summary>
public class RemoveMedicineOrderItemResponse : IFeatureResponse
{
    public RemoveMedicineOrderItemResponseStatusCode StatusCode { get; init; }
}
