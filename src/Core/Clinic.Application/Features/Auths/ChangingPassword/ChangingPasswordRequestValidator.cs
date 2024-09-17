using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.ChangingPassword;

/// <summary>
///     ChangingPassword request validator.
/// </summary>
public sealed class ChangingPasswordRequestValidator
    : FeatureRequestValidator<ChangingPasswordRequest, ChangingPasswordResponse>
{
    public ChangingPasswordRequestValidator()
    {
        RuleFor(expression: request => request.NewPassword)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);

        RuleFor(expression: request => request.ResetPasswordToken).NotEmpty();
    }
}
