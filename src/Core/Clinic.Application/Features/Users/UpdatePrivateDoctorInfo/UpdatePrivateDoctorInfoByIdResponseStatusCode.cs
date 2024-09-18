namespace Clinic.Application.Features.Users.UpdatePrivateDoctorInfoById;

/// <summary>
///     UpdateUserById Response Status Code
/// </summary>
public enum UpdatePrivateDoctorInfoByIdResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    //UPLOAD_IMAGE_FAIL,
    //ADDRESS_IS_NOT_CORRECT_FORMAT
}
