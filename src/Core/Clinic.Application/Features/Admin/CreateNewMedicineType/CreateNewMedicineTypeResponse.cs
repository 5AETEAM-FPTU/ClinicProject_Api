using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

/// <summary>
///     CreateNewMedicineType
/// </summary>
public sealed class CreateNewMedicineTypeResponse : IFeatureResponse
{
    public CreateNewMedicineTypeResponseStatusCode StatusCode { get; set; }

}
