using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllSpecialty;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllSpecialty.HttpResponseMapper;

/// <summary>
///     GetAllSpecialty http response
/// </summary>
internal sealed class GetAllSpecialtyHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllSpecialtyResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllSpecialtyResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}

