using System.Collections.Generic;
using System;
using Clinic.Application.Features.Doctors.GetMedicalReportById;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Doctors.GetMedicalReportById.HttpResponseMapper;

public class GetMedicalReportByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetMedicalReportByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}