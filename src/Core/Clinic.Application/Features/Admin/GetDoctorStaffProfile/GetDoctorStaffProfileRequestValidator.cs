using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Admin.GetDoctorStaffProfile;

internal class GetDoctorStaffProfileRequestValidator
    : FeatureRequestValidator<GetDoctorStaffProfileRequest, GetDoctorStaffProfileResponse>
{
    public GetDoctorStaffProfileRequestValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .Must(x => Guid.TryParse(x.ToString(), out _))
            .WithMessage("Invalid doctor id");
    }
}
