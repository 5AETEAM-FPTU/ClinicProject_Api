using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.UpdateMedicineGroupById;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicineGroupById.HttpResponseMapper;

public class UpdateMedicineGroupByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
