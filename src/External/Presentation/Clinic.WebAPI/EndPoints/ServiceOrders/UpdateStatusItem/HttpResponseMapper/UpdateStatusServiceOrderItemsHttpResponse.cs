using Clinic.Application.Features.ServiceOrders.UpdateStatusItem;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.UpdateStatusItem.HttpResponseMapper;

/// <summary>
///     UpdateStatusServiceOrderItems HttpResponse
/// </summary>
public sealed class UpdateStatusServiceOrderItemsHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateStatusServiceOrderItemsResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}
