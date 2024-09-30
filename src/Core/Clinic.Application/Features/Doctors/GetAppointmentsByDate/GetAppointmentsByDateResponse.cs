using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Application.Features.Doctors.GetAppointmentsByDate;

/// <summary>
///     GetSchedulesByDate Response Status Code
/// </summary>
public class GetAppointmentsByDateResponse : IFeatureResponse
{
    public GetAppointmentsByDateResponseStatusCode StatusCode { get; init; }

    public IEnumerable<AppointmentDTO> AppointmentDTOResponse { get; init; }

    public sealed class AppointmentDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public PatientDTO PatientDTOResponse { get; set; }
        public sealed class PatientDTO
        {
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public Gender Gender { get; set; }
            public DateTime DOB { get; set; }
        }

        public ScheduleDTO ScheduleDTOResponse { get; set; }
        public sealed class ScheduleDTO
        {
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }
        }
    }
}
