using System.Collections.Generic;
using System;
using Clinic.Application.Features.Users.GetConsultationOverview;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Users.GetConsultationOverview.HttpResponseMapper;

public class GetConsultationOverviewHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetConsultationOverviewResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}

