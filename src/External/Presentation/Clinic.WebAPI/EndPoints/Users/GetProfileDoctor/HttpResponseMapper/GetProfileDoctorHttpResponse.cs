using Clinic.Application.Commons.Abstractions.GetProfileUser;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Users.GetProfileDoctor.HttpResponseMapper;

public class GetProfileDoctorHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetProfileUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
