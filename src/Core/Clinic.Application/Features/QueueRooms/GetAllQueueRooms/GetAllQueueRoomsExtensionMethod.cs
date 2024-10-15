namespace Clinic.Application.Features.QueueRooms.GetAllQueueRooms;

/// <summary>
///     GetAllQueueRooms ExtensionMethod.
/// </summary>
public static class GetAllQueueRoomsExtensionMethod
{
    public static string ToAppCode(this GetAllQueueRoomsResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllQueueRooms)}Feature: {statusCode}";
    }
}
