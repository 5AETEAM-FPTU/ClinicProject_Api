using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword Request
/// </summary>
public class ForgotPasswordRequest : IFeatureRequest<ForgotPasswordResponse>
{
    public string Email { get; init; }
}
