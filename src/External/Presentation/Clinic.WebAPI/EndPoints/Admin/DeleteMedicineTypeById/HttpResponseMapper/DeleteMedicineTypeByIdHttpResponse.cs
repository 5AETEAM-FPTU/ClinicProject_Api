using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Clinic.Application.Features.Admin.DeleteMedicineTypeById;

namespace Clinic.WebAPI.EndPoints.Admin.DeleteMedicineTypeById.HttpResponseMapper;

/// <summary>
///     DeleteMedicineTypeById http response
/// </summary>
public sealed class DeleteMedicineTypeByIdHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        DeleteMedicineTypeByIdResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
