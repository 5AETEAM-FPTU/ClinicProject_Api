using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetAppointmentsByDate Response Status Code
/// </summary>
public class GetAppointmentsByDateResponse : IFeatureResponse
{
    public GetAppointmentsByDateResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public List<AppointmentDTO> Appointment { get; init; }

        public sealed class AppointmentDTO
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public PatientDTO Patient { get; set; }

            public sealed class PatientDTO
            {
                public string Avatar { get; set; }
                public string FullName { get; set; }
                public string PhoneNumber { get; set; }
                public GenderDTO Gender { get; set; }

                public sealed class GenderDTO
                {
                    public Guid Id { get; set; }
                    public string Name { get; set; }
                    public string Constant { get; set; }
                }

                public DateTime DOB { get; set; }
            }

            public ScheduleDTO Schedule { get; set; }

            public sealed class ScheduleDTO
            {
                public DateTime StartDate { get; set; }

                public DateTime EndDate { get; set; }
            }

            public AppointmentStatusDTO AppointmentStatus { get; set; }

            public sealed class AppointmentStatusDTO
            {
                public Guid Id { get; set; }
                public string StatusName { get; set; }
                public string Constant { get; set; }
            }

            public bool IsHadMedicalReport { get; set; }
            public Guid MedicalReportId { get; set; }
        }
    }
}
