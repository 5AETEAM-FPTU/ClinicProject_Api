
using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllAppointmentStatus;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllAppointmentStatus.HttpResponseMapper;

/// <summary>
///     GetAllAppointmentStatus http response
/// </summary>
internal sealed class GetAllAppointmentStatusHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllAppointmentStatusResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllAppointmentStatusResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
