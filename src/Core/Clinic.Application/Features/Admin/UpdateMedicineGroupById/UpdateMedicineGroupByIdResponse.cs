using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.UpdateMedicineGroupById;

/// <summary>
///     UpdateMedicineGroupById Response
/// </summary>
public class UpdateMedicineGroupByIdResponse : IFeatureResponse
{
    public UpdateMedicineGroupByIdResponseStatusCode StatusCode { get; init; }
}
