using Clinic.Application.Features.Schedules.RemoveAllSchedules;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Schedules.RemoveAllSchedules.HttpResponseMapper;

/// <summary>
///     UpdateSchedule http response
/// </summary>
public sealed class RemoveAllSchedulesHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveAllSchedulesResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
