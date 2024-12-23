﻿using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetRecentPending;

/// <summary>
///     GetRecentPending Response
/// </summary>
public class GetRecentPendingResponse : IFeatureResponse
{
    public GetRecentPendingResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<AppointmentDetail> Appointment { get; init; }

        public sealed class AppointmentDetail
        {
            public Guid Id { get; init; }
            public string FullName { get; init; }
            public string AvatarUrl { get; init; }
            public DateTime StartDate { get; init; }
            public DateTime EndDate { get; init; }
            public DateTime CreatedDate { get; init; }
        }
    }
}
