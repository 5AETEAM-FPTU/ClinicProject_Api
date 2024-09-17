using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;
using FluentValidation;

namespace Clinic.Application.Features.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken request validator.
/// </summary>
public sealed class RefreshAccessTokenRequestValidator
    : FeatureRequestValidator<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    public RefreshAccessTokenRequestValidator()
    {
        RuleFor(expression: request => request.RefreshToken)
            .NotEmpty()
            .MinimumLength(minimumLength: RefreshToken.MetaData.RefreshTokenValue.MinLength);
    }
}
