namespace Clinic.Application.Features.Appointments.GetUserBookedAppointment;

/// <summary>
///     GetUserBookedAppointment Response Status Code
/// </summary>
public enum GetUserBookedAppointmentResponseStatusCode
{
    APPOINTMENTS_IS_NOT_FOUND,
    USER_IS_LOCKED_OUT,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    ROLE_IS_NOT_USER
}

