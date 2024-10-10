namespace Clinic.WebAPI.EndPoints.Payments.HandleRedirectURL.HttpResponseMapper;

/// <summary>
///     Manages <see cref="HandleRedirectURLHttpResponseManager"/>
/// </summary>
internal static class HandleRedirectURLHttpResponseMapper
{
    private static HandleRedirectURLHttpResponseManager _manager = new();

    internal static HandleRedirectURLHttpResponseManager Get() => _manager ??= new();
}
