using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetAbsentForStaff;

/// <summary>
///     GetAbsentForStaff Response
/// </summary>
public class GetAbsentForStaffResponse : IFeatureResponse
{
    public GetAbsentForStaffResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<AppointmentDetail> Appointment { get; init; }

        public sealed class AppointmentDetail
        {
            public Guid Id { get; init; }
            public string Description { get; init; }
            public Schedule Schedules { get; init; }
            public UserDetail Patients { get; init; }
            public Status AppointmentStatus { get; init; }

            public sealed class Schedule
            {
                public Guid ScheduleId { get; init; }
                public DateTime StartDate { get; init; }
                public DateTime EndDate { get; init; }
            }

            public sealed class UserDetail
            {
                public Guid UserId { get; init; }
                public string FullName { get; init; }
                public string AvatarUrl { get; init; }
                public string Gender { get; init; }
                public string PhoneNumber { get; init; }
                public DateTime DOB { get; init; }
            }

            public sealed class Status
            {
                public Guid Id { get; init; }
                public string StatusName { get; init; }
                public string Constant { get; init; }
            }
        }
    }
}
