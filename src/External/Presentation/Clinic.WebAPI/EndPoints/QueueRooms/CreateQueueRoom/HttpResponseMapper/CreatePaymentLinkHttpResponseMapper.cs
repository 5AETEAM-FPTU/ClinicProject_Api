namespace Clinic.WebAPI.EndPoints.QueueRooms.CreateQueueRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="CreateQueueRoomResponse"/> and <see cref="CreateQueueRoomHttpResponse"/>
/// </summary>
internal static class CreateQueueRoomHttpResponseMapper
{
    private static CreateQueueRoomHttpResponseManager _manager = new();

    internal static CreateQueueRoomHttpResponseManager Get() => _manager ??= new();
}
