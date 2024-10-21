using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;

public sealed class GetDoctorScheduleByDateRequestValidator
    : FeatureRequestValidator<GetDoctorScheduleByDateRequest, GetDoctorScheduleByDateResponse>
{
    public GetDoctorScheduleByDateRequestValidator()
    {
        RuleFor(expression: request => request.Date).NotEmpty();
        RuleFor(expression: request => request.DoctorId)
            .NotEmpty()
            .Must(predicate: request => Guid.TryParse(request.ToString(), out _));
    }
}
