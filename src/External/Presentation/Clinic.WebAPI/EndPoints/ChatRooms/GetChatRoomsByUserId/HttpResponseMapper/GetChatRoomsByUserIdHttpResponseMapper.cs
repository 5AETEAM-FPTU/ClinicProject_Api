namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByUserId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatRoomsByUserIdResponse"/> and <see cref="GetChatRoomsByUserIdHttpResponse"/>
/// </summary>
internal static class GetChatRoomsByUserIdHttpResponseMapper
{
    private static GetChatRoomsByUserIdHttpResponseManager _manager = new();

    internal static GetChatRoomsByUserIdHttpResponseManager Get() => _manager ??= new();
}
