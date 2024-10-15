using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

public class CreateNewMedicineTypeRequest : IFeatureRequest<CreateNewMedicineTypeResponse>
{
    public string Name { get; init; }
    public string Constant { get; init; }
}
