namespace Clinic.Application.Features.Users.UpdateUserAvatar;

/// <summary>
///     UpdateUserById Response Status Code
/// </summary>
public enum UpdateUserAvatarResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    UPLOAD_IMAGE_FAIL,
    //ADDRESS_IS_NOT_CORRECT_FORMAT
}
