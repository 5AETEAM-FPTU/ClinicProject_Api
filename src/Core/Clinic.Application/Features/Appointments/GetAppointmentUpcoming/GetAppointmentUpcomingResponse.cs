using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetAppointmentUpcoming;

/// <summary>
///     GetAppointmentUpcoming Response
/// </summary>
public class GetAppointmentUpcomingResponse : IFeatureResponse
{
    public GetAppointmentUpcomingResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public DateTime UpcomingDate { get; init; }

        public int TotalAppointmentedPation { get; init; }
    }
}
