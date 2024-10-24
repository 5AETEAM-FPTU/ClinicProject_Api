namespace Clinic.WebAPI.EndPoints.ChatRooms.SwitchToEndChatRoom.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="SwitchToEndChatRoomResponse"/> and <see cref="SwitchToEndChatRoomHttpResponse"/>
/// </summary>
internal static class SwitchToEndChatRoomHttpResponseMapper
{
    private static SwitchToEndChatRoomHttpResponseManager _manager = new();

    internal static SwitchToEndChatRoomHttpResponseManager Get() => _manager ??= new();
}
