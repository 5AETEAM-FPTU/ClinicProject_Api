namespace Clinic.Application.Features.Auths.LoginByAdmin;

/// <summary>
///     LoginByAdmin response status code.
/// </summary>
public enum LoginByAdminResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    USER_PASSWORD_IS_NOT_CORRECT,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    EMAIL_IS_NOT_CONFIRMED,
    ROLE_IS_NOT_ADMIN
}
