namespace Clinic.WebAPI.EndPoints.ChatRooms.AssignChatRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="AssignChatRoomResponse"/> and <see cref="AssignChatRoomHttpResponse"/>
/// </summary>
internal static class AssignChatRoomHttpResponseMapper
{
    private static AssignChatRoomHttpResponseManager _manager = new();

    internal static AssignChatRoomHttpResponseManager Get() => _manager ??= new();
}
