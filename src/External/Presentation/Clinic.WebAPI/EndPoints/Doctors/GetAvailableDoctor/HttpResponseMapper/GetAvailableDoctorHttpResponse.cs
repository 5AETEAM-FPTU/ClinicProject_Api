using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Commons.Abstractions.GetProfileUser;

namespace Clinic.WebAPI.EndPoints.Doctors.GetAvailableDoctor.HttpResponseMapper;

public class GetAvailableDoctorHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        GetProfileUserResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
