using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;

namespace Clinic.Application.Features.Enums.GetAllDoctorStaffType;

public class GetAllDoctorStaffTypeResponse : IFeatureResponse
{
    public GetAllDoctorStaffTypeResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<DoctorStaffType> DoctorStaffTypes { get; init; }

        public sealed class DoctorStaffType
        {
            public Guid Id { get; init; }
            public string TypeName { get; init; }
            public string Constant { get; init; }
        }
    }
}
