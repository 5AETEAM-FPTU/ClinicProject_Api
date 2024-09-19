using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Users.GetAllUser;

namespace Clinic.WebAPI.EndPoints.Users.GetAllUser.HttpResponseMapper;

/// <summary>
///     GetAllDoctors http response
/// </summary>
internal sealed class GetAllUserHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetAllUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public GetAllUserResponse.Body Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}

