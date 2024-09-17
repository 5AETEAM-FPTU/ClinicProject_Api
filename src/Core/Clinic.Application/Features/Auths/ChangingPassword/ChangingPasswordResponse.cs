using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Auths.ChangingPassword;

/// <summary>
///     ChangingPassword Response
/// </summary>
public class ChangingPasswordResponse : IFeatureResponse
{
    public ChangingPasswordResponseStatusCode StatusCode { get; init; }
}
