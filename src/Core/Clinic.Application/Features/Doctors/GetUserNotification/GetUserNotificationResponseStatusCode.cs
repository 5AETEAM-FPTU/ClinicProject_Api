namespace Clinic.Application.Features.Doctors.GetUserNotification;

/// <summary>
///     GetUserNotification Response Status Code
/// </summary>
public enum GetUserNotificationResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    ROLE_IS_NOT_DOCTOR_STAFF,
    USER_ID_NOT_FOUND,
}
