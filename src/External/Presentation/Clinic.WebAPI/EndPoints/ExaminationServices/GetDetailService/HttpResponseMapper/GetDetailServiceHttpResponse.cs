﻿using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.ExaminationServices.GetDetailService;

namespace Clinic.WebAPI.EndPoints.ExaminationServices.GetDetailService.HttpResponseMapper;

public class GetDetailServiceHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetDetailServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
