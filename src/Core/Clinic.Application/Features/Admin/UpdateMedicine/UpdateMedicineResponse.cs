using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.UpdateMedicine;

/// <summary>
///     UpdateMedicine Response
/// </summary>
public class UpdateMedicineResponse : IFeatureResponse
{
    public UpdateMedicineResponseStatusCode StatusCode { get; init; }

}
