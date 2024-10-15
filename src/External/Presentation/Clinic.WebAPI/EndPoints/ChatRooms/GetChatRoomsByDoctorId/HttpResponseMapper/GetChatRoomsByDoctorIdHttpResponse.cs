using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ChatRooms.GetChatRoomsByDoctorId;

namespace Clinic.WebAPI.EndPoints.ChatRooms.GetChatRoomsByDoctorId.HttpResponseMapper;

/// <summary>
///     Response of GetChatRoomsByDoctorId
/// </summary>
public sealed class GetChatRoomsByDoctorIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetChatRoomsByDoctorIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
