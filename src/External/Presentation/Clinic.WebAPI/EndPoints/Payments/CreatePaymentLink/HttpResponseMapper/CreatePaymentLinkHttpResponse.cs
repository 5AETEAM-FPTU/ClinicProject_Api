using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.OnlinePayments.CreatePaymentLink;

namespace Clinic.WebAPI.EndPoints.Payments.CreatePaymentLink.HttpResponseMapper;

/// <summary>
///     Response of CreatePaymentLink
/// </summary>
public sealed class CreatePaymentLinkHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
