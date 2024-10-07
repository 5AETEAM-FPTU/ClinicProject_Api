namespace Clinic.Application.Features.Appointments.UpdateAppointmentStatus;
public enum UpdateAppointmentStatusResponseStatusCode {
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    INPUT_VALIDATION_FAIL,
    UNAUTHORIZE,
    FORBIDEN_ACCESS,
    APPOINTMENT_IS_NOT_FOUND,
    STATUS_IS_NOT_ACCEPTABLE
}