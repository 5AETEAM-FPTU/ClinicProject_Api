using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.Logout;

/// <summary>
///     Logout response.
/// </summary>
public sealed class LogoutResponse : IFeatureResponse
{
    public LogoutResponseStatusCode StatusCode { get; init; }
}
