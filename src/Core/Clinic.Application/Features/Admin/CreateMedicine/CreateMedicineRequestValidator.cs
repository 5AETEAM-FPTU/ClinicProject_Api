using Clinic.Application.Commons.Abstractions;
using FluentValidation;
using System;

namespace Clinic.Application.Features.Admin.CreateMedicine;

public sealed class CreateMedicineRequestValidator
    : FeatureRequestValidator<CreateMedicineRequest, CreateMedicineResponse>
{
    public CreateMedicineRequestValidator()
    {
        RuleFor(expression: request => request.MedicineGroupId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("ScheduleId must be a valid");
        RuleFor(expression: request => request.MedicineTypeId)
            .NotEmpty()
            .Must(predicate: id => Guid.TryParse(id.ToString(), out _))
            .WithMessage("ScheduleId must be a valid");
        RuleFor(expression: request => request.Ingredient).NotEmpty();
        RuleFor(expression: request => request.MedicineName).NotEmpty();
        RuleFor(expression: request => request.Manufacture).NotEmpty();
    }
}

