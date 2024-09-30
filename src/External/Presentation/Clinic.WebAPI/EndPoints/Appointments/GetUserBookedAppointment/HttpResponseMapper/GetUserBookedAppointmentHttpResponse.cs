using System.Collections.Generic;
using System;
using Clinic.Application.Features.Appointments.GetUserBookedAppointment;
using System.Text.Json.Serialization;

namespace Clinic.WebAPI.EndPoints.Appointments.GetUserBookedAppointment.HttpResponseMapper;

public class GetUserBookedAppointmentHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } = GetUserBookedAppointmentResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}

