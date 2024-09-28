using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Enums.GetAllSpecialty;

/// <summary>
///    GetAllSpecialtyResponse
/// </summary>
public class GetAllSpecialtyResponse : IFeatureResponse
{
    public GetAllSpecialtyResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Specialty> Specialties { get; init; }

        public sealed class Specialty
        {
            public Guid Id { get; init; }
            public string SpecialtyName { get; init; }

            public string Constant { get; init; }
        }
    }
}

