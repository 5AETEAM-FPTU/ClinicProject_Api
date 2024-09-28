using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;

namespace Clinic.Application.Features.Enums.GetAllAppointmentStatus;

/// <summary>
///     GetAllAppointmentStatus Response
/// </summary>
public class GetAllAppointmentStatusResponse : IFeatureResponse
{
    public GetAllAppointmentStatusResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<AppointmentStatus> AppointmentStatuses { get; init; }

        public sealed class AppointmentStatus
        {
            public Guid Id { get; init; }
            public string StatusName { get; init; }

            public string Constant { get; init; }
        }
    }
}
