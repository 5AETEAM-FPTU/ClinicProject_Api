using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByUserId;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByUserId.HttpResponseMapper;

/// <summary>
///     Response of GetChatRoomsByUserId
/// </summary>
public sealed class GetChatRoomsByUserIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetChatRoomsByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
