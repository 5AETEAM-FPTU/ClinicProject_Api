using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.RegisterAsUser;

/// <summary>
///     RegisterAsUser request validator.
/// </summary>
public sealed class RegisterAsUserRequestValidator
    : FeatureRequestValidator<RegisterAsUserRequest, RegisterAsUserResponse>
{
    public RegisterAsUserRequestValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(minimumLength: User.MetaData.Email.MinLength)
            .MaximumLength(maximumLength: User.MetaData.Email.MaxLength);

        RuleFor(expression: request => request.FullName)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength)
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength);

        RuleFor(expression: request => request.Password)
            .NotEmpty()
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength)
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength);
    }
}
