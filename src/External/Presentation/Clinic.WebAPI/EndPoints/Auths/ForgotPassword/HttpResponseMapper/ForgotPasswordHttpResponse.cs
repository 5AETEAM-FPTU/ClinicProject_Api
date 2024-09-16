using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Auths.ForgotPassword;

namespace Clinic.WebAPI.EndPoints.Auths.ForgotPassword.HttpResponseMapper;

/// <summary>
///     ForgotPassword http response
/// </summary>
internal sealed class ForgotPasswordHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        ForgotPasswordResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
