namespace Clinic.WebAPI.EndPoints.QueueRooms.GetAllQueueRooms.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetAllQueueRoomsResponse"/> and <see cref="GetAllQueueRoomsHttpResponse"/>
/// </summary>
internal static class GetAllQueueRoomsHttpResponseMapper
{
    private static GetAllQueueRoomsHttpResponseManager _manager = new();

    internal static GetAllQueueRoomsHttpResponseManager Get() => _manager ??= new();
}
