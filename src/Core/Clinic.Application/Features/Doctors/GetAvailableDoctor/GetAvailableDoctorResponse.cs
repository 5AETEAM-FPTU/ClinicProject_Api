using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetAvailableDoctor;

/// <summary>
///     GetAvailableDoctor Response
/// </summary>
public class GetAvailableDoctorResponse : IFeatureResponse
{
    public GetAvailableDoctorResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Doctor> Doctors { get; init; }

        public sealed class Doctor
        {
            public Guid DoctorId { get; set; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }
        }
    }
}
