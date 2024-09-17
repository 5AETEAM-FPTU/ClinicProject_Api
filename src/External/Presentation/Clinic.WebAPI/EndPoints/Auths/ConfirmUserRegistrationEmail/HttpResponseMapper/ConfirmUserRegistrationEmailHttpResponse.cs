using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Auths.ConfirmUserRegistrationEmail;

namespace Clinic.WebAPI.EndPoints.Auths.ConfirmUserRegistrationEmail.HttpResponseMapper;

/// <summary>
///     ConfirmUserRegistrationEmail http response
/// </summary>
internal sealed class ConfirmUserRegistrationEmailHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        ConfirmUserRegistrationEmailResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
