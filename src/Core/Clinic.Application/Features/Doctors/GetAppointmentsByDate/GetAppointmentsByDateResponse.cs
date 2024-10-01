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
        public List<AppointmentDTO> AppointmentDTOResponse { get; init; }

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
                public Gender Gender { get; set; }
                public DateTime DOB { get; set; }
            }

            public ScheduleDTO Schedule { get; set; }
            public sealed class ScheduleDTO
            {
                public DateTime StartDate { get; set; }

                public DateTime EndDate { get; set; }
            }
        }
    }
}
