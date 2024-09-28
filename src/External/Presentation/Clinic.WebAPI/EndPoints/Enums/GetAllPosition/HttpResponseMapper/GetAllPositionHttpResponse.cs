using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllPosition;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllPosition.HttpResponseMapper;

/// <summary>
///     GetAllPosition http response
/// </summary>
internal sealed class GetAllPositionHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllPositionResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllPositionResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
