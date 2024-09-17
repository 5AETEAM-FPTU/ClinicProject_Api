using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.RegisterAsUser;

/// <summary>
///     RegisterAsUser response.
/// </summary>
public sealed class RegisterAsUserResponse : IFeatureResponse
{
    public RegisterAsUserResponseStatusCode StatusCode { get; init; }
}
