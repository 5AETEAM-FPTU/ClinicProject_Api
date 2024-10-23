using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.RemovedDoctorTemporarily;

namespace Clinic.WebAPI.EndPoints.Admin.RemovedDoctorTemporarily.HttpResponseMapper;

/// <summary>
///     RemovedDoctorTemporarily http response
/// </summary>
public sealed class RemovedDoctorTemporarilyHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemovedDoctorTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
