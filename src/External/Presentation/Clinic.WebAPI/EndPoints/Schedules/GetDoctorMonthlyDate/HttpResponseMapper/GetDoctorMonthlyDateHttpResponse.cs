using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Schedules.GetDoctorMonthlyDate;
using Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorMonthlyDate.HttpResponseMapper;

public class GetDoctorMonthlyDateHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetDoctorMonthlyDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}
