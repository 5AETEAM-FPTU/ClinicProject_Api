using Clinic.Application.Features.MedicineOrders.UpdateNoteMedicineOrder;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.MedicineOrders.UpdateNoteMedicineOrder.HttpResponseMapper;

/// <summary>
///     UpdateNoteMedicineOrder http response
/// </summary>
public sealed class UpdateNoteMedicineOrderHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateNoteMedicineOrderResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}
