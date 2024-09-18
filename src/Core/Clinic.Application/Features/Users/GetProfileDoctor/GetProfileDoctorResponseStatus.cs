namespace Clinic.Application.Features.Users.GetProfileDoctor;

/// <summary>
///     GetProfileDoctor Response Status Code
/// </summary>
public enum GetProfileDoctorResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
}
