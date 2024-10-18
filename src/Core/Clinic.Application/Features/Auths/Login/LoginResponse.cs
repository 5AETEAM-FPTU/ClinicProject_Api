using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Login response.
/// </summary>
public sealed class LoginResponse : IFeatureResponse
{
    public LoginResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string CallAccessToken { get; init; }

        public string RefreshToken { get; init; }

        public UserCredential User { get; init; }

        public sealed class UserCredential
        {
            public string Email { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }
        }
    }
}
