using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ExaminationServices.GetAllServices;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetAllServices.HttpResponseMapper;

public class GetAllServicesHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetAllServicesResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
