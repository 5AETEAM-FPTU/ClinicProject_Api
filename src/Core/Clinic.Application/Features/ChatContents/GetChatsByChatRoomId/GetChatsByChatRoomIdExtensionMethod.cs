namespace Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     Extension Method for GetChatsByChatRoomId features.
/// </summary>
public static class GetChatsByChatRoomIdExtensionMethod
{
    public static string ToAppCode(this GetChatsByChatRoomIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetChatsByChatRoomId)}Feature: {statusCode}";
    }
}
