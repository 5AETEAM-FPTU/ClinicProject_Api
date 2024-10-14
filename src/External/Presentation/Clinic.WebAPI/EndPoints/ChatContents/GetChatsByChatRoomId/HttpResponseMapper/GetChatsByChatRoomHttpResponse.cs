using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ChatContents.GetChatsByChatRoomId;

namespace Clinic.WebAPI.EndPoints.ChatContents.GetChatsByChatRoomId.HttpResponseMapper;

/// <summary>
///     Response of GetChatsByChatRoomId
/// </summary>
public sealed class GetChatsByChatRoomIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetChatsByChatRoomIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
