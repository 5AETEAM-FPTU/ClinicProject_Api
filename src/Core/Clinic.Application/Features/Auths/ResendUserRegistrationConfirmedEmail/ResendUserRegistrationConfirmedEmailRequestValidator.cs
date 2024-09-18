using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail request validator.
/// </summary>
public sealed class ResendUserRegistrationConfirmedEmailRequestValidator
    : FeatureRequestValidator<
        ResendUserRegistrationConfirmedEmailRequest,
        ResendUserRegistrationConfirmedEmailResponse
    >
{
    public ResendUserRegistrationConfirmedEmailRequestValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(minimumLength: User.MetaData.UserName.MinLength)
            .MaximumLength(maximumLength: User.MetaData.UserName.MaxLength);
    }
}
