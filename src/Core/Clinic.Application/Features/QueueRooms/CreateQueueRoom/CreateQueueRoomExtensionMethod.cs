namespace Clinic.Application.Features.QueueRooms.CreateQueueRoom;

/// <summary>
///     CreateQueueRoomExtensionMethod.
/// </summary>
public static class CreateQueueRoomExtensionMethod
{
    public static string ToAppCode(this CreateQueueRoomResponseStatusCode statusCode)
    {
        return $"{nameof(CreateQueueRoom)}Feature: {statusCode}";
    }
}
