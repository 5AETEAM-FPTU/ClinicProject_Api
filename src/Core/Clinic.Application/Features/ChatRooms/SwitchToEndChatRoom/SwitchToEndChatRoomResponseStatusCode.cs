namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom Response StatusCode.
/// </summary>
public enum SwitchToEndChatRoomResponseStatusCode
{
    CHAT_ROOM_IS_NOT_FOUND,
    ROLE_IS_NOT_DOCTOR_OR_STAFF,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
