namespace Clinic.WebAPI.EndPoints.Auths.RefreshAccessToken.HttpResponseMapper;

/// <summary>
///     RefreshAccessToken extension method
/// </summary>
internal static class RefreshAccessTokenHttpResponseMapper
{
    private static RefreshAccessTokenHttpResponseManager _RefreshAccessTokenHttpResponseManager;

    internal static RefreshAccessTokenHttpResponseManager Get()
    {
        return _RefreshAccessTokenHttpResponseManager ??= new();
    }
}
