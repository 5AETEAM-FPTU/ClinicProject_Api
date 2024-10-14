using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.DeleteMedicineTypeById;

/// <summary>
///     DeleteMedicineTypeById Response Status Code
/// </summary>
public class DeleteMedicineTypeByIdResponse : IFeatureResponse
{
    public DeleteMedicineTypeByIdResponseStatusCode StatusCode { get; init; }
}
