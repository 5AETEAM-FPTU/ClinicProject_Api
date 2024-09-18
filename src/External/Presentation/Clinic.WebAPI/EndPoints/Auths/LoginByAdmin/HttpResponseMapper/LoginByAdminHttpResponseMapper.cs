namespace Clinic.WebAPI.EndPoints.Auths.LoginByAdmin.HttpResponseMapper;

/// <summary>
///     LoginByAdmin extension method
/// </summary>
internal static class LoginByAdminHttpResponseMapper
{
    private static LoginByAdminHttpResponseManager _LoginByAdminHttpResponseManager;

    internal static LoginByAdminHttpResponseManager Get()
    {
        return _LoginByAdminHttpResponseManager ??= new();
    }
}
