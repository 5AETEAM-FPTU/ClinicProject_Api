﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Schedules.GetDoctorScheduleByDate;
using Clinic.Application.Features.Schedules.GetSchedulesByDate;

namespace Clinic.WebAPI.EndPoints.Schedules.GetDoctorScheduleByDate.HttpResponseMapper;

public class GetDoctorScheduleByDateHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetDoctorScheduleByDateResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}