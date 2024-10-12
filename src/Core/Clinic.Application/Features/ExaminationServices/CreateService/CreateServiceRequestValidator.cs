using Clinic.Application.Commons.Abstractions;
using FluentValidation;
using System;

namespace Clinic.Application.Features.ExaminationServices.CreateService;

/// <summary>
///     CreateService Validator
/// </summary>
public sealed class CreateServiceRequestValidator
    : FeatureRequestValidator<CreateServiceRequest, CreateServiceResponse>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(expression: request => request.Code)
           .NotEmpty();
        RuleFor(expression: request => request.Name)
            .NotEmpty();
        RuleFor(expression: request => request.Price)
            .NotEmpty();
    }
}

