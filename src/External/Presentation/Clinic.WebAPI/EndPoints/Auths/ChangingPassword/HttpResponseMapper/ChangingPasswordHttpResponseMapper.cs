namespace Clinic.WebAPI.EndPoints.Auths.ChangingPassword.HttpResponseMapper;

/// <summary>
///     ChangingPassword extension method
/// </summary>
internal static class ChangingPasswordHttpResponseMapper
{
    private static ChangingPasswordHttpResponseManager _ChangingPasswordHttpResponseManager;

    internal static ChangingPasswordHttpResponseManager Get()
    {
        return _ChangingPasswordHttpResponseManager ??= new();
    }
}
