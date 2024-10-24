using Clinic.Application.Features.Feedbacks.DoctorGetAllFeedbacks;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Feedbacks.DoctorGetAllFeedbacks.HttpResponseMapper;

/// <summary>
///     DoctorGetAllFeedback http response
/// </summary>

public class DoctorGetAllFeedBackHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        DoctorGetAllFeedBackResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
    public object Body { get; set; } = new();

}
