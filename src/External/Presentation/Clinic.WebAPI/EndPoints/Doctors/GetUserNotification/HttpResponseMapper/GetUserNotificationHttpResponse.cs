using Clinic.Application.Features.Doctors.GetUserNotification;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Doctors.GetUserNotification.HttpResponseMapper;

public class GetUserNotificationHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetUserNotificationResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
