namespace Clinic.Application.Commons.Abstractions.GetProfileUser;

/// <summary>
///     GetProfileUser Response Status Code
/// </summary>
public enum GetProfileUserResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
}
