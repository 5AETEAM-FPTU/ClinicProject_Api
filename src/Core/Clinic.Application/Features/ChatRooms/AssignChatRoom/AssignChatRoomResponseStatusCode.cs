namespace Clinic.Application.Features.ChatRooms.AssignChatRoom;

/// <summary>
///     AssignChatRoom Response StatusCode.
/// </summary>
public enum AssignChatRoomResponseStatusCode
{
    PATIENT_ID_NOT_FOUND,
    DOCTOR_ID_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
}
