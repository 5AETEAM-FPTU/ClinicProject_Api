using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Users.UpdateDoctorDescription;
using Clinic.Application.Features.Users.UpdateDoctorAchievement;

namespace Clinic.WebAPI.EndPoints.Users.UpdateDoctorAchievement.HttpResponseMapper;

public class UpdateDoctorAchievementHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = UpdateDoctorAchievementByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
