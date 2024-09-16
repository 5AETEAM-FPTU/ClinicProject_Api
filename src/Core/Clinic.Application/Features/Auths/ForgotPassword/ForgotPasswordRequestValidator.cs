using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword request validator.
/// </summary>
public sealed class ForgotPasswordRequestValidator
    : FeatureRequestValidator<ForgotPasswordRequest, ForgotPasswordResponse>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(maximumLength: User.MetaData.Email.MaxLength)
            .MinimumLength(minimumLength: User.MetaData.Email.MinLength);
    }
}
