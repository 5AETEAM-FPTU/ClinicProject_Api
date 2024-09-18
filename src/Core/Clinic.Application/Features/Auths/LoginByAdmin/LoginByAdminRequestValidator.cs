using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.LoginByAdmin;

/// <summary>
///     LoginByAdmin request validator.
/// </summary>
public sealed class LoginByAdminRequestValidator
    : FeatureRequestValidator<LoginByAdminRequest, LoginByAdminResponse>
{
    public LoginByAdminRequestValidator()
    {
        RuleFor(expression: request => request.Username)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength);

        RuleFor(expression: request => request.Password)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);
    }
}
