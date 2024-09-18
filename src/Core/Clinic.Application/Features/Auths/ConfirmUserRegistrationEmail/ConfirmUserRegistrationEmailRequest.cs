using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Request
/// </summary>
public class ConfirmUserRegistrationEmailRequest
    : IFeatureRequest<ConfirmUserRegistrationEmailResponse>
{
    [BindFrom("token")]
    public string UserRegistrationEmailConfirmedTokenAsBase64 { get; init; }
}
