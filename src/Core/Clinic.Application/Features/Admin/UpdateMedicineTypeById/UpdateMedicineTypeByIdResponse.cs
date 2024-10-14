using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.UpdateMedicineTypeById;

/// <summary>
///     UpdateMedicineTypeById Response
/// </summary>
public class UpdateMedicineTypeByIdResponse : IFeatureResponse
{
    public UpdateMedicineTypeByIdResponseStatusCode StatusCode { get; init; }
}
