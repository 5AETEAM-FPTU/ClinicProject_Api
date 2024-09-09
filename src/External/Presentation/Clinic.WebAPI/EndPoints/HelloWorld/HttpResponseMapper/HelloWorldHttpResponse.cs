using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.HelloWorld;

namespace Clinic.WebAPI.EndPoints.HelloWorld.HttpResponseMapper;

/// <summary>
///     HelloWorld http response
/// </summary>
internal sealed class HelloWorldHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        HelloWorldResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
