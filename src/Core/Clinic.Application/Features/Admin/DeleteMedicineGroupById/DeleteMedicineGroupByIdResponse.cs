using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.DeleteMedicineGroupById;

/// <summary>
///     DeleteMedicineGroupById Response Status Code
/// </summary>
public class DeleteMedicineGroupByIdResponse : IFeatureResponse
{
    public DeleteMedicineGroupByIdResponseStatusCode StatusCode { get; init; }
}
