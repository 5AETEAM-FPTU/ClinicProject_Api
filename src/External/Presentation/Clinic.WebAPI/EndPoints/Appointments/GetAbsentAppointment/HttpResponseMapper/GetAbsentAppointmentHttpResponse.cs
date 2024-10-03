﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Appointments.GetAbsentAppointment;

namespace Clinic.WebAPI.EndPoints.Appointments.GetAbsentAppointment.HttpResponseMapper;

public class GetAbsentAppointmentHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAbsentAppointmentResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
