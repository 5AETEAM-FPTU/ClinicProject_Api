using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ExaminationServices.GetAvailableServices;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAvailableServices.HttpResponseMapper;

/// <summary>
///     GetAvailableSerivces http response
/// </summary>
public class GetAvailableServicesHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetAvailableServicesResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
