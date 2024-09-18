using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.LoginByAdmin;

/// <summary>
///     LoginByAdmin Request
/// </summary>
public class LoginByAdminRequest : IFeatureRequest<LoginByAdminResponse>
{
    public string Username { get; init; }
    public string Password { get; init; }
    public bool IsRemember { get; set; }
}
