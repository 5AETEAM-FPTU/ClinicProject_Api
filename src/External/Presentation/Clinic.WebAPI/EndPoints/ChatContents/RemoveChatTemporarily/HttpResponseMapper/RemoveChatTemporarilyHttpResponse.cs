using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ChatContents.RemoveChatContentTemporarily;

namespace Clinic.WebAPI.EndPoints.ChatContents.RemoveChatTemporarily.HttpResponseMapper;

/// <summary>
///     Response of RemoveChatTemporarily
/// </summary>
public sealed class RemoveChatTemporarilyHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveChatContentTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
