namespace Clinic.WebAPI.EndPoints.Auths.LoginWithGoogle.HttpResponseMapper;

/// <summary>
///     LoginWithGoogle extension method
/// </summary>
internal static class LoginWithGoogleHttpResponseMapper
{
    private static LoginWithGoogleHttpResponseManager _LoginWithGoogleHttpResponseManager;

    internal static LoginWithGoogleHttpResponseManager Get()
    {
        return _LoginWithGoogleHttpResponseManager ??= new();
    }
}
