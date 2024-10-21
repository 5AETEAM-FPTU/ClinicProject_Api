using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Doctors.GetIdsDoctor;

/// <summary>
///     GetIdsDoctor Response
/// </summary>
public class GetIdsDoctorResponse : IFeatureResponse
{
    public GetIdsDoctorResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Doctor> Doctors { get; init; }

        public sealed class Doctor
        {
            public Guid Id { get; set; }
        }
    }
}
