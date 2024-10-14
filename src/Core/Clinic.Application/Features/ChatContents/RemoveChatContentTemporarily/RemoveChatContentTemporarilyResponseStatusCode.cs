namespace Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarily Response StatusCode.
/// </summary>
public enum RemoveChatContentTemporarilyResponseStatusCode
{
    CHATCONTENT_IS_NOT_FOUND,
    CHATCONTENT_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    FORBIDEN_ACCESS,
}
