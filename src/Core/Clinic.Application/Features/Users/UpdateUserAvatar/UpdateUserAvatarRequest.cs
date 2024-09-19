using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdateUserAvatar;

/// <summary>
///     GetProfileUser Request
/// </summary>
public class UpdateUserAvatarRequest : IFeatureRequest<UpdateUserAvatarResponse> 
{
    public string Avatar { get; set; }

}
