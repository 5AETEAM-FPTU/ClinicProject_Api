using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

/// <summary>
///     CreateNewMedicineGroup
/// </summary>
public sealed class CreateNewMedicineGroupResponse : IFeatureResponse
{
    public CreateNewMedicineGroupResponseStatusCode StatusCode { get; set; }

}
