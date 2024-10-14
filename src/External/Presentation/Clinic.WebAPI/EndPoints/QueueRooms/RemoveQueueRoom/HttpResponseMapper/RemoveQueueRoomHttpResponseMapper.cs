namespace Clinic.WebAPI.EndPoints.QueueRooms.RemoveQueueRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="RemoveQueueRoomResponse"/> and <see cref="RemoveQueueRoomHttpResponse"/>
/// </summary>
internal static class RemoveQueueRoomHttpResponseMapper
{
    private static RemoveQueueRoomHttpResponseManager _manager = new();

    internal static RemoveQueueRoomHttpResponseManager Get() => _manager ??= new();
}
