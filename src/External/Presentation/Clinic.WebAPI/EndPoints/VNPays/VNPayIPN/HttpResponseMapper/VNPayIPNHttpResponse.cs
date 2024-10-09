using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Features.Auths.ChangingPassword;
using Clinic.Application.Features.VNPays.CreatePaymentLink;
using Clinic.Application.Features.VNPays.VNPayIPN;

namespace Clinic.WebAPI.EndPoints.VNPayIPN.HttpResponseMapper;

public sealed class VNPayIPNHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        VNPayIPNResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
