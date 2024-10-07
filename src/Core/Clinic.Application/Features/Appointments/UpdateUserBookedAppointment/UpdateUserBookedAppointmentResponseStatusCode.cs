namespace Clinic.Application.Features.Appointments.UpdateUserBookedAppointment;

public enum UpdateUserBookedAppointmentResponseStatusCode
{
    APPOINTMENTS_IS_NOT_FOUND,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    ROLE_IS_NOT_ACCEPTABLE,
    DATABASE_OPERATION_FAIL,
    UNAUTHORIZE,
    FORBIDEN_ACCESS,
    APPOINTMENT_ONLY_UPDATE_ONCE
}