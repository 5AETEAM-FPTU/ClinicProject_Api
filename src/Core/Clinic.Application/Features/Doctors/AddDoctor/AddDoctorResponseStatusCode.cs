namespace Clinic.Application.Features.Doctors.AddDoctor;

/// <summary>
///     AddDoctor Response Status Code
/// </summary>
public enum AddDoctorResponseStatusCode
{
    USER_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    ROLE_IS_NOT_A_DOCTOR,
    EMAIL_DOCTOR_EXITS,
    DOCTOR_STAFF_TYPE_GUID_IS_NOT_EXIST,
    FORBIDEN_ACCESS,
}
