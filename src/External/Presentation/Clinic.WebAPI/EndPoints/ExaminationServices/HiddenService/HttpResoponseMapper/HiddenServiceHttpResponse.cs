using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ExaminationServices.HiddenService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.HiddenService.HttpResoponseMapper;

/// <summary>
///     Hidden Service HttResponse
/// </summary>

public class HiddenServiceHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        HiddenServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
