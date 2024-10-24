namespace Clinic.Application.Features.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     SwitchToEndChatRoom ExtensionMethod.
/// </summary>
public static class SwitchToEndChatRoomExtensionMethod
{
    public static string ToAppCode(this SwitchToEndChatRoomResponseStatusCode statusCode)
    {
        return $"{nameof(SwitchToEndChatRoom)}Feature: {statusCode}";
    }
}
