
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserAvatar;

/// <summary>
///     GetProfileUser Response
/// </summary>
public class UpdateUserAvatarResponse : IFeatureResponse
{
    public UpdateUserAvatarResponseStatusCode StatusCode { get; init; }

}
