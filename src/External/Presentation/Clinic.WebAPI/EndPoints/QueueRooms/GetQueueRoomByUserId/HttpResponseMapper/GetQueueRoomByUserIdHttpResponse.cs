using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.QueueRooms.GetQueueRoomByUserId;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetQueueRoomByUserId.HttpResponseMapper;

/// <summary>
///     Response of GetQueueRoomByUserId
/// </summary>
public sealed class GetQueueRoomByUserIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetQueueRoomByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
