namespace Clinic.WebAPI.EndPoints.Auths.ConfirmUserRegistrationEmail.HttpResponseMapper;

/// <summary>
///     ConfirmUserRegistrationEmail extension method
/// </summary>
internal static class ConfirmUserRegistrationEmailHttpResponseMapper
{
    private static ConfirmUserRegistrationEmailHttpResponseManager _ConfirmUserRegistrationEmailHttpResponseManager;

    internal static ConfirmUserRegistrationEmailHttpResponseManager Get()
    {
        return _ConfirmUserRegistrationEmailHttpResponseManager ??= new();
    }
}
