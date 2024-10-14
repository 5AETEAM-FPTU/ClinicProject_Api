namespace Clinic.WebAPI.EndPoints.QueueRooms.GetQueueRoomByUserId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetQueueRoomByUserIdResponse"/> and <see cref="GetQueueRoomByUserIdHttpResponse"/>
/// </summary>
internal static class GetQueueRoomByUserIdHttpResponseMapper
{
    private static GetQueueRoomByUserIdHttpResponseManager _manager = new();

    internal static GetQueueRoomByUserIdHttpResponseManager Get() => _manager ??= new();
}
