namespace Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     GetChatRoomsByDoctorIdExtensionMethod.
/// </summary>
public static class GetChatRoomsByDoctorIdExtensionMethod
{
    public static string ToAppCode(this GetChatRoomsByDoctorIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetChatRoomsByDoctorId)}Feature: {statusCode}";
    }
}
