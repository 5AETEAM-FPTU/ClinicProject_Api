namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarilyExtensionMethod.
/// </summary>
public static class RemoveChatContentTemporarilyExtensionMethod
{
    public static string ToAppCode(this RemoveChatContentTemporarilyResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveChatContentTemporarily)}Feature: {statusCode}";
    }
}
