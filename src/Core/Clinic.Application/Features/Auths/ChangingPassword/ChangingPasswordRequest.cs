using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ChangingPassword;

/// <summary>
///     ChangingPassword Request
/// </summary>
public class ChangingPasswordRequest : IFeatureRequest<ChangingPasswordResponse>
{
    public string Email { get; set; }

    public string NewPassword { get; init; }

    public string ResetPasswordToken { get; init; }
}
