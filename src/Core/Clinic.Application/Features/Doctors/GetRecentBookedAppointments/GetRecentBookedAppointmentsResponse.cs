using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetRecentBookedAppointments;

/// <summary>
///     GetAppointmentsByDate Response Status Code
/// </summary>
public class GetRecentBookedAppointmentsResponse : IFeatureResponse
{
    public GetRecentBookedAppointmentsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }
    public sealed class Body
    {
        public List<AppointmentDTO> Appointments { get; init; }

        public sealed class AppointmentDTO
        {
            public Guid Id { get; set; }
            public PatientDTO Patient { get; set; }
            public sealed class PatientDTO
            {
                public string Avatar { get; set; }
                public string FullName { get; set; }
            }

            public ScheduleDTO Schedule { get; set; }
            public sealed class ScheduleDTO
            {
                public DateTime StartDate { get; set; }

                public DateTime EndDate { get; set; }
            }

            public DateTime CreatedAt { get; set; }
        }
    }
}
