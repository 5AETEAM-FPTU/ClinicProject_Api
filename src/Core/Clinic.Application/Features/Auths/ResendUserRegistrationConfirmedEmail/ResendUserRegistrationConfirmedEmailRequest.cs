using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ResendUserRegistrationConfirmedEmail;

/// <summary>
///     ResendUserRegistrationConfirmedEmail Request
/// </summary>
public class ResendUserRegistrationConfirmedEmailRequest
    : IFeatureRequest<ResendUserRegistrationConfirmedEmailResponse>
{
    public string Email { get; set; }
}
