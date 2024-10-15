namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatsByChatRoomId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatsByChatRoomResponse"/> and <see cref="GetChatsByChatRoomHttpResponse"/>
/// </summary>
internal static class GetChatsByChatRoomIdHttpResponseMapper
{
    private static GetChatsByChatRoomIdHttpResponseManager _manager = new();

    internal static GetChatsByChatRoomIdHttpResponseManager Get() => _manager ??= new();
}
