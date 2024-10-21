using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;

public sealed class GetDoctorMonthlyDateValidator
    : FeatureRequestValidator<GetDoctorMonthlyDateRequest, GetDoctorMonthlyDateResponse>
{
    public GetDoctorMonthlyDateValidator()
    {
        RuleFor(expression: request => request.Year).NotEmpty();
        RuleFor(expression: request => request.Month)
            .NotEmpty()
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");
        ;
    }
}
