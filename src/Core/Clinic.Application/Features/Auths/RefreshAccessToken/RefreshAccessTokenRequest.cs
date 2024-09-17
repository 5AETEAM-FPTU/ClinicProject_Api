using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken Request
/// </summary>
public class RefreshAccessTokenRequest : IFeatureRequest<RefreshAccessTokenResponse>
{
    public string RefreshToken { get; set; }
}
