﻿using Clinic.Application.Features.Doctors.GetAppointmentsByDate;
using Clinic.Application.Features.Doctors.GetRecentBookedAppointments;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentBookedAppointments.HttpResponseMapper;

/// <summary>
///     GetSchedulesByDate http response
/// </summary>
public sealed class GetRecentBookedAppointmentsHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetRecentBookedAppointmentsResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}
