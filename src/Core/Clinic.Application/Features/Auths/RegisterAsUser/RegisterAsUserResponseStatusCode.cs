namespace Clinic.Application.Features.Auths.RegisterAsUser;

/// <summary>
///     RegisterAsUser response status code.
/// </summary>
public enum RegisterAsUserResponseStatusCode
{
    USERNAME_IS_EXIST,
    EMAIL_IS_EXIST,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PASSWORD_IS_NOT_VAID,
    SENDING_USER_CONFIRMATION_MAIL_FAIL
}
