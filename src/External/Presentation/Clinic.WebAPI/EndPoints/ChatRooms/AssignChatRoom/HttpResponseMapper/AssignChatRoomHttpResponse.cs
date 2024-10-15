using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ChatRooms.AssignChatRoom;

namespace Clinic.WebAPI.EndPoints.ChatRooms.AssignChatRoom.HttpResponseMapper;

/// <summary>
///     Response of AssignChatRoom
/// </summary>
public sealed class AssignChatRoomHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        AssignChatRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
