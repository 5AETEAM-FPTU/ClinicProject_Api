namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByDoctorId.HttpResponseMapper;

/// <summary>
///     Manages the mapping between <see cref="GetChatRoomsByDoctorIdResponse"/> and <see cref="GetChatRoomsByDoctorIdHttpResponse"/>
/// </summary>
internal static class GetChatRoomsByDoctorIdHttpResponseMapper
{
    private static GetChatRoomsByDoctorIdHttpResponseManager _manager = new();

    internal static GetChatRoomsByDoctorIdHttpResponseManager Get() => _manager ??= new();
}
