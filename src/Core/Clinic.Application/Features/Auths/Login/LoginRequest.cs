using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Login Request
/// </summary>
public class LoginRequest : IFeatureRequest<LoginResponse>
{
    public string Username { get; init; }
    public string Password { get; init; }
    public bool IsRemember { get; set; }
}
