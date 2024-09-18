using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Abstractions.UpdatePrivateDoctorInfoById;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;

public sealed class UpdatePrivateDoctorInfoByIdRequestValidator 
    : FeatureRequestValidator<UpdatePrivateDoctorInfoByIdRequest, UpdatePrivateDoctorInfoByIdResponse>
{
    //public UpdatePrivateDoctorInfoByIdRequestValidator()
    //{
    //    RuleFor(x => x.).NotEmpty().Matches(@"^\+?\d{10,15}$");
    //}
}
