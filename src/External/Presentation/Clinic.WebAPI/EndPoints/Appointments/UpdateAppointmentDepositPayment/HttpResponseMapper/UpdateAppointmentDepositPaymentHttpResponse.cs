using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Clinic.Application.Features.Appointments.UpdateAppointmentDepositPayment;

public class UpdateAppointmentDepositPaymentHttpResponse {
   
    [JsonIgnore]
    public int HttpCode { get; set; }

    public string AppCode { get; init; } =
        UpdateAppointmentDepositPaymentResponseStatusCode.OPERATION_SUCCESS.ToAppCode();

    public DateTime ResponseTime { get; init; } =
        TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time")
        );

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = []; 

}