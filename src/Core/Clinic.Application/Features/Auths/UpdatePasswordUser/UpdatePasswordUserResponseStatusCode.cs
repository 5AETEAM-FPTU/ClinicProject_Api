namespace Clinic.Application.Features.Auths.UpdatePasswordUser;

/// <summary>
///     UpdatePasswordUser Response Status Code
/// </summary>
public enum UpdatePasswordUserResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    NEW_PASSWORD_IS_NOT_VALID
}
