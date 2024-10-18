namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     GetChatRoomsByUserIdExtensionMethod.
/// </summary>
public static class GetChatRoomsByUserIdExtensionMethod
{
    public static string ToAppCode(this GetChatRoomsByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetChatRoomsByUserId)}Feature: {statusCode}";
    }
}
