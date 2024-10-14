namespace Clinic.Application.Features.QueueRooms.RemoveQueueRoom;

/// <summary>
///     RemoveQueueRoom ExtensionMethod.
/// </summary>
public static class RemoveQueueRoomExtensionMethod
{
    public static string ToAppCode(this RemoveQueueRoomResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveQueueRoom)}Feature: {statusCode}";
    }
}
