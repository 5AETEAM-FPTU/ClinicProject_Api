using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.CreateNewMedicineType;

namespace Clinic.WebAPI.EndPoints.Admin.CreateNewMedicineType.HttpResponseMapper;

/// <summary>
/// Create new Medicine Type http response
/// </summary>

public class CreateNewMedicineTypeHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        CreateNewMedicineTypeResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
