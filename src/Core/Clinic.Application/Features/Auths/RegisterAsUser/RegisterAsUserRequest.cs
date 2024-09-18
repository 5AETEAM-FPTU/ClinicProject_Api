using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.RegisterAsUser;

/// <summary>
///     RegisterAsUser Request
/// </summary>
public class RegisterAsUserRequest : IFeatureRequest<RegisterAsUserResponse>
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
