using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.RemoveMedicineTemporarily;

namespace Clinic.WebAPI.EndPoints.Admin.RemoveMedicineTemporarily.HttpResponseMapper;

/// <summary>
///     RemoveMedicineTemporarily http response
/// </summary>
public sealed class RemoveMedicineTemporarilyHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        RemoveMedicineTemporarilyResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
