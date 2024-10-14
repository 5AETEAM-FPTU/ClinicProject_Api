namespace Clinic.WebAPI.EndPoints.ChatRooms.RemoveChatTemporarily.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="RemoveChatTemporarilyResponse"/> and <see cref="RemoveChatTemporarilyHttpResponse"/>
/// </summary>
internal static class RemoveChatTemporarilyHttpResponseMapper
{
    private static RemoveChatTemporarilyHttpResponseManager _manager = new();

    internal static RemoveChatTemporarilyHttpResponseManager Get() => _manager ??= new();
}
