namespace Clinic.Application.Features.Auths.ForgotPassword;

/// <summary>
///     ForgotPassword response status code.
/// </summary>
public enum ForgotPasswordResponseStatusCode
{
    USER_WITH_EMAIL_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SENDING_USER_RESET_PASSWORD_MAIL_FAIL,
    UNEXPIRED_TOKEN_EXISTS
}
