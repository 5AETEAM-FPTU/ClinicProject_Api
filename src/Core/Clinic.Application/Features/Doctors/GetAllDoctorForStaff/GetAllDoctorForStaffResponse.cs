using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForStaff;

/// <summary>
///     GetAllDoctorForStaffResponse
/// </summary>
public class GetAllDoctorForStaffResponse : IFeatureResponse
{
    public GetAllDoctorForStaffResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<UserDetail> userDetails { get; init; }

        public sealed class UserDetail
        {
            public Guid Id { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public ResponseGender Gender { get; init; }

            public sealed class ResponseGender
            {
                public Guid Id { get; init; }
                public string GenderName { get; init; }
                public string GenderConstant { get; init; }
            }

            public IEnumerable<ResponseSpecialties> Specialties { get; init; }

            public sealed class ResponseSpecialties
            {
                public Guid Id { get; init; }
                public string SpecialtyName { get; init; }
                public string SpecialtyConstant { get; init; }
            }

            public bool IsOnDuty { get; init; }
        }
    }
}
