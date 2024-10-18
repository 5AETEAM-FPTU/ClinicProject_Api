using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Notification.CreateRetreatmentNotification;

namespace Clinic.WebAPI.EndPoints.Notification.CreateRetreatmentNotification.HttpResponseMapper;

/// <summary>
/// Create new Notification http response
/// </summary>

public class CreateRetreatmentNotificationHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateRetreatmentNotificationResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
