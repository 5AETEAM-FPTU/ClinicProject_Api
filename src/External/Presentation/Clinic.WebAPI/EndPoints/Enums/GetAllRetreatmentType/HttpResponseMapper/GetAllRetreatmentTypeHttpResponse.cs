using System.Collections.Generic;
using System;
using Clinic.Application.Features.Enums.GetAllRetreatmentType;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Enums.GetAllRetreatmentType.HttpResponseMapper;

/// <summary>
///     GetAllRetreatmentType http response
/// </summary>
internal sealed class GetAllRetreatmentTypeHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllRetreatmentTypeResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllRetreatmentTypeResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
