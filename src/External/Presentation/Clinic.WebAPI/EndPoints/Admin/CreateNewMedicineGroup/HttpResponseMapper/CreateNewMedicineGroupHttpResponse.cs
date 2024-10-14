using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.CreateNewMedicineGroup;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineGroup.HttpResponseMapper;

/// <summary>
/// Create new Medicine Group http response
/// </summary>

public class CreateNewMedicineGroupHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateNewMedicineGroupResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}

