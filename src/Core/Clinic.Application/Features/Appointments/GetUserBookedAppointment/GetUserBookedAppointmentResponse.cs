using System.Collections.Generic;
using System;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Appointments.GetUserBookedAppointment;

/// <summary>
///     GetUserBookedAppointment Response
/// </summary>
public class GetUserBookedAppointmentResponse : IFeatureResponse
{
    public GetUserBookedAppointmentResponseStatusCode StatusCode { get; init; }

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
            public Doctor DoctorDetails { get; init; }
            public sealed class Doctor
            {
                public Guid DoctorId { get; init; }
                public string FullName { get; init; }
                public string AvatarUrl { get; init; }
                public IEnumerable<Specialty> Specialties { get; init; }
                public sealed class Specialty
                {
                    public Guid Id { get; init; }
                    public string Name { get; init; }
                    public string Constant { get; init; }
                }
            }
        }
    }
}
