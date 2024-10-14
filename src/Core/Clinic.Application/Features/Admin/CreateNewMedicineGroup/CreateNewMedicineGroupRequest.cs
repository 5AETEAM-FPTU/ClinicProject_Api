using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

public class CreateNewMedicineGroupRequest : IFeatureRequest<CreateNewMedicineGroupResponse>
{
    public string Name { get; init; }
    public string Constant { get; init; }
}
