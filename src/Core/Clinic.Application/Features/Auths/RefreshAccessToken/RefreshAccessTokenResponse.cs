using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken response.
/// </summary>
public sealed class RefreshAccessTokenResponse : IFeatureResponse
{
    public RefreshAccessTokenResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }
    }
}
