﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Auths.ChangingPassword;

namespace Clinic.WebAPI.EndPoints.Auths.ChangingPassword.HttpResponseMapper;

/// <summary>
///     ChangingPassword http response
/// </summary>
internal sealed class ChangingPasswordHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        ChangingPasswordResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
