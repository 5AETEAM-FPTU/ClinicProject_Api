using Clinic.Application.Commons.Abstractions.GetProfileUser;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileUser.Common;

internal sealed class GetProfileUserStateBag
{
    internal GetProfileUserRequest AppRequest { get; } = new();
}
