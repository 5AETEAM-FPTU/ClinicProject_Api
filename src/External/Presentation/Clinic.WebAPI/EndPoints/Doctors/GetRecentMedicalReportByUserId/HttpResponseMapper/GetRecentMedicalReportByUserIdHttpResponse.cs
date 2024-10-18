﻿using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Doctors.GetRecentMedicalReportByUserId;

namespace Clinic.WebAPI.EndPoints.Doctors.GetRecentMedicalReportByUserId.HttpResponseMapper;

/// <summary>
///     GetRecentMedicalReportByUserId http response
/// </summary>
public sealed class GetRecentMedicalReportByUserIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetRecentMedicalReportByUserIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}