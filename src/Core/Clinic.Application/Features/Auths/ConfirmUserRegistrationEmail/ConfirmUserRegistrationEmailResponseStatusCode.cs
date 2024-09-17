namespace Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail response status code.
/// </summary>
public enum ConfirmUserRegistrationEmailResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    USER_HAS_CONFIRMED_REGISTRATION_EMAIL,
    USER_IS_TEMPORARILY_REMOVED,
    USER_IS_NOT_FOUND,
    TOKEN_IS_NOT_CORRECT,
}
