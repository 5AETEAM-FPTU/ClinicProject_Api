using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.LoginWithGoogle;

/// <summary>
///     LoginWithGoogle request validator.
/// </summary>
public sealed class LoginWithGoogleRequestValidator
    : FeatureRequestValidator<LoginWithGoogleRequest, LoginWithGoogleResponse>
{
    public LoginWithGoogleRequestValidator()
    {
        RuleFor(expression: request => request.IdToken).NotEmpty();
    }
}
