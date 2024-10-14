namespace Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;

/// <summary>
///     GetQueueRoomByUserId ExtensionMethod.
/// </summary>
public static class GetQueueRoomByUserIdExtensionMethod
{
    public static string ToAppCode(this GetQueueRoomByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetQueueRoomByUserId)}Feature: {statusCode}";
    }
}
