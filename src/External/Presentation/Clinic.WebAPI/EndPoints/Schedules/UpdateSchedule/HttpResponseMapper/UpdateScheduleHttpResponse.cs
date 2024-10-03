using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Schedules.CreateSchedules;
using Clinic.Application.Features.Schedules.UpdateSchedule;

namespace Clinic.WebAPI.EndPoints.Schedules.UpdateSchedule.HttpResponseMapper;

/// <summary>
///     UpdateSchedule http response
/// </summary>
public sealed class UpdateScheduleHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateScheduleResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
