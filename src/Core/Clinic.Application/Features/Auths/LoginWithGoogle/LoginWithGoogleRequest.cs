using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.LoginWithGoogle;

/// <summary>
///     LoginWithGoogle Request
/// </summary>
public class LoginWithGoogleRequest : IFeatureRequest<LoginWithGoogleResponse>
{
    public string IdToken { get; init; }
}
