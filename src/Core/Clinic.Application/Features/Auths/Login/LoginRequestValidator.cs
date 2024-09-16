using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Login request validator.
/// </summary>
public sealed class LoginRequestValidator : FeatureRequestValidator<LoginRequest, LoginResponse>
{
    public LoginRequestValidator()
    {
        RuleFor(expression: request => request.Username)
            .NotEmpty()
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength);

        RuleFor(expression: request => request.Password)
            .NotEmpty()
            .Matches(expression: @"^(?=.*\d)(?=.*[A-Z]).+$")
            .MaximumLength(maximumLength: User.MetaData.Password.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Password.MinLength);
    }
}
