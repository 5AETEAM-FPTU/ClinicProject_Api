using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail response.
/// </summary>
public sealed class ResendUserRegistrationConfirmedEmailResponse : IFeatureResponse
{
    public ResendUserRegistrationConfirmedEmailResponseStatusCode StatusCode { get; init; }
}
