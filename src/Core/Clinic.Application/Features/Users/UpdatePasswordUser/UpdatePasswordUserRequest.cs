using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Users.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser Request
/// </summary>
public class UpdatePasswordUserRequest : IFeatureRequest<UpdatePasswordUserResponse>
{
    public string CurrentPassword { get; init; }

    public string NewPassword { get; init; }
}
