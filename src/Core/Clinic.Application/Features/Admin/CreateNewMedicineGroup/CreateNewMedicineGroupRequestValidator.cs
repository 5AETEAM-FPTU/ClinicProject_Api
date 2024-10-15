using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Admin.CreateNewMedicineGroup;

public sealed class CreateNewMedicineGroupRequestValidator
    : FeatureRequestValidator<CreateNewMedicineGroupRequest, CreateNewMedicineGroupResponse>
{
    public CreateNewMedicineGroupRequestValidator()
    {
        RuleFor(expression: request => request.Name).NotEmpty();
        RuleFor(expression: request => request.Constant).NotEmpty();
    }
}
