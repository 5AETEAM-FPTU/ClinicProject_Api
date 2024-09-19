using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

public sealed class UpdatePrivateDoctorInfoByIdRequestValidator
    : FeatureRequestValidator<UpdatePrivateDoctorInfoByIdRequest, UpdatePrivateDoctorInfoByIdResponse>
{
    public UpdatePrivateDoctorInfoByIdRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{10,15}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber)) // Only validate if PhoneNumber is not null or empty
            .WithMessage("Phone number must be between 10 and 15 digits and can optionally start with a '+'.");
    }
}
