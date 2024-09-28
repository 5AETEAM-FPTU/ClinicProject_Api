namespace Clinic.Application.Features.Users.UpdateUserPrivateInfo;

/// <summary>
///     UpdateUserById Response Status Code
/// </summary>
public enum UpdateUserPrivateInfoResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    GENDER_ID_IS_NOT_FOUND,
}
