using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.DeleteMedicineGroupById;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineGroupById.HttpResponseMapper;

/// <summary>
///     DeleteMedicineGroupById http response
/// </summary>
public sealed class DeleteMedicineGroupByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        DeleteMedicineGroupByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
