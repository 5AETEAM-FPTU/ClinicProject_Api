using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword Response
/// </summary>
public class ForgotPasswordResponse : IFeatureResponse
{
    public ForgotPasswordResponseStatusCode StatusCode { get; init; }
}
