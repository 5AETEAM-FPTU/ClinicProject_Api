using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail request validator.
/// </summary>
public sealed class ConfirmUserRegistrationEmailRequestValidator
    : FeatureRequestValidator<
        ConfirmUserRegistrationEmailRequest,
        ConfirmUserRegistrationEmailResponse
    >
{
    public ConfirmUserRegistrationEmailRequestValidator()
    {
        RuleFor(expression: request => request.UserRegistrationEmailConfirmedTokenAsBase64)
            .NotEmpty();
    }
}
