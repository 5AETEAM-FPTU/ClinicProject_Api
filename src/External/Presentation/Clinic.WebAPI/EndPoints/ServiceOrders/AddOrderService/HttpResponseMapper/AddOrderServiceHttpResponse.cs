using Clinic.Application.Features.ServiceOrders.AddOrderService;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.ServiceOrders.AddOrderService.HttpResponseMapper;

/// <summary>
///     AddOrderService http response
/// </summary>
public sealed class AddOrderServiceHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        AddOrderServiceResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public IEnumerable<string> ErrorMessages { get; init; } = [];

    public object Body { get; set; } = new();
}
