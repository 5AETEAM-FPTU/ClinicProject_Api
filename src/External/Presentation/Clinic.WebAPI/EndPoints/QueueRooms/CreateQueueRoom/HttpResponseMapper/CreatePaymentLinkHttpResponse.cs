using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.QueueRooms.CreateQueueRoom;

namespace Clinic.WebAPI.EndPoints.QueueRooms.CreateQueueRoom.HttpResponseMapper;

/// <summary>
///     Response of CreateQueueRoom
/// </summary>
public sealed class CreateQueueRoomHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateQueueRoomResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
