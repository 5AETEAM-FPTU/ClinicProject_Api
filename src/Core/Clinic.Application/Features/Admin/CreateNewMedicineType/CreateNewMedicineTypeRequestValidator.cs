using Clinic.Application.Commons.Abstractions;
using FluentValidation;


namespace Clinic.Application.Features.Admin.CreateNewMedicineType;

public sealed class CreateNewMedicineTypeRequestValidator
    : FeatureRequestValidator<CreateNewMedicineTypeRequest, CreateNewMedicineTypeResponse>
{
    public CreateNewMedicineTypeRequestValidator()
    {
        RuleFor(expression: request => request.Name).NotEmpty();
        RuleFor(expression: request => request.Constant).NotEmpty();
    }
}
