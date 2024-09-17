namespace Clinic.Application.Features.Auths.RefreshAccessToken;

/// <summary>
///     RefreshAccessToken response status code.
/// </summary>
public enum RefreshAccessTokenResponseStatusCode
{
    REFRESH_TOKEN_IS_NOT_FOUND,
    REFRESH_TOKEN_IS_EXPIRED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
