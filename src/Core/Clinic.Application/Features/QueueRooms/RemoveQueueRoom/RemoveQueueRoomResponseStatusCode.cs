namespace Clinic.Application.Features.QueueRooms.RemoveQueueRoom;

/// <summary>
///     RemoveQueueRoom Response StatusCode.
/// </summary>
public enum RemoveQueueRoomResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    QUEUE_ROOM_NOT_FOUND,
    THIS_UER_IS_NOT_QUEUE_ROOM_OWNER,
}
