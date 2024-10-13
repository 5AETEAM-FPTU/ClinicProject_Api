using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.DeleteMedicineById;

/// <summary>
///     DeleteMedicineById Response Status Code
/// </summary>
public class DeleteMedicineByIdResponse : IFeatureResponse
{
    public DeleteMedicineByIdResponseStatusCode StatusCode { get; init; }
}
