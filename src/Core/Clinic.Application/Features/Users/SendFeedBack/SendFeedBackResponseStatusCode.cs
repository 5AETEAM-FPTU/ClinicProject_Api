namespace Clinic.Application.Features.Users.SendFeedBack;

public enum SendFeedBackResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    FEEDBACK_IS_ALREADY_SENT,
    APPOINTMENT_IS_NOT_COMPLETED
}
