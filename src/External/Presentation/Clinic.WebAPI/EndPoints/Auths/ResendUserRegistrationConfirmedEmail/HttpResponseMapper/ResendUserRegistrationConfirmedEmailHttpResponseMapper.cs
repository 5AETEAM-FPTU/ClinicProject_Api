namespace Clinic.WebAPI.EndPoints.Auths.ResendUserRegistrationConfirmedEmail.HttpResponseMapper;

/// <summary>
///     ResendUserRegistrationConfirmedEmail extension method
/// </summary>
internal static class ResendUserRegistrationConfirmedEmailHttpResponseMapper
{
    private static ResendUserRegistrationConfirmedEmailHttpResponseManager _ResendUserRegistrationConfirmedEmailHttpResponseManager;

    internal static ResendUserRegistrationConfirmedEmailHttpResponseManager Get()
    {
        return _ResendUserRegistrationConfirmedEmailHttpResponseManager ??= new();
    }
}
