namespace Clinic.Application.Features.Auths.Login;

/// <summary>
///     Login response status code.
/// </summary>
public enum LoginResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    USER_PASSWORD_IS_NOT_CORRECT,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    EMAIL_IS_NOT_CONFIRMED
}
