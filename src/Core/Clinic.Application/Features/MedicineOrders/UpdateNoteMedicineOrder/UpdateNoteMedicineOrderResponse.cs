using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;

/// <summary>
///     UpdateMedicineOrderItem Response
/// </summary>
public class UpdateNoteMedicineOrderResponse : IFeatureResponse
{
    public UpdateNoteMedicineOrderResponseStatusCode StatusCode { get; init; }
}
