namespace Clinic.Application.Features.Notification.CreateRetreatmentNotification;

public enum CreateRetreatmentNotificationResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    UNAUTHORIZE,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
    NOTIFICATION_ALREADY_EXISTED,
    SMS_NOTIFICATION_FAIL
}
