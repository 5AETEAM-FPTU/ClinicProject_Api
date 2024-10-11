using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ExaminationServices.CreateService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.CreateService.HttpResoponseMapper;

/// <summary>
/// Create new Medicine http response
/// </summary>

public class CreateServiceHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
