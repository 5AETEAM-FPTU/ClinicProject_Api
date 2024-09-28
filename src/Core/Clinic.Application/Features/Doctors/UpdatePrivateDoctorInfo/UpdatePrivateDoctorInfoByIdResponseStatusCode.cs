namespace Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;

/// <summary>
///     UpdateUserById Response Status Code
/// </summary>
public enum UpdatePrivateDoctorInfoByIdResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    GENDER_ID_IS_NOT_FOUND,
    POSITION_ID_IS_NOT_FOUND,
    SPECIALTY_ID_IS_NOT_FOUND
}
