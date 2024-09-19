using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateUserPrivateInfoResponse : IFeatureResponse
{
    public UpdateUserPrivateInfoResponseStatusCode StatusCode { get; init; }

}
