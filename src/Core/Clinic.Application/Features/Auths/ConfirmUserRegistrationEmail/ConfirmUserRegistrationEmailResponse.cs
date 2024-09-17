using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Response
/// </summary>
public class ConfirmUserRegistrationEmailResponse : IFeatureResponse
{
    public ConfirmUserRegistrationEmailResponseStatusCode StatusCode { get; init; }

    public string ResponseBodyAsHtml { get; init; }
}
