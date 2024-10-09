namespace Clinic.Application.Features.Auths.LoginWithGoogle;

/// <summary>
///     LoginWithGoogle response status code.
/// </summary>
public enum LoginWithGoogleResponseStatusCode
{
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    EMAIL_IS_NOT_CONFIRMED,
    INVALID_GOOGLE_TOKEN
}
