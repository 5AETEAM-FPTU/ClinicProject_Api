

namespace Clinic.WebAPI.EndPoints.Users.UpdateUserAvatar.HttpResponseMapper;

internal static class UpdateUserAvatarHttpResponseMapper
{
    private static UpdateUserAvatarHttpResponseManager updateUserAvatarHttpResponseManager;

    internal static UpdateUserAvatarHttpResponseManager Get()
    {
        return updateUserAvatarHttpResponseManager ??= new();
    }
}
