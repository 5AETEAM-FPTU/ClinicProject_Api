using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetAbsentAppointment;

/// <summary>
///     GetAbsentAppointment Response
/// </summary>
public class GetAbsentAppointmentResponse : IFeatureResponse
{
    public GetAbsentAppointmentResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<AppointmentDetail> Appointment { get; init; }

        public sealed class AppointmentDetail
        {
            public Guid AppointmentId { get; init; }
            public Guid ScheduleId { get; init; }
            public DateTime StartDate { get; init; }
            public DateTime EndDate { get; init; }
            public UserDetail Users { get; init; }

            public sealed class UserDetail
            {
                public Guid UserId { get; init; }
                public string FullName { get; init; }
                public string AvatarUrl { get; init; }
                public string Gender { get; init; }
                public string PhoneNumber { get; init; }
                public string Description { get; init; }
                public DateTime DOB { get; init; }
            }
        }
    }
}
