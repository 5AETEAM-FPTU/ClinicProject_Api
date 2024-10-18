using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.QueueRooms.GetAllQueueRooms;

namespace Clinic.WebAPI.EndPoints.QueueRooms.GetAllQueueRooms.HttpResponseMapper;

/// <summary>
///     Response of GetAllQueueRooms
/// </summary>
public sealed class GetAllQueueRoomsHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllQueueRoomsResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
