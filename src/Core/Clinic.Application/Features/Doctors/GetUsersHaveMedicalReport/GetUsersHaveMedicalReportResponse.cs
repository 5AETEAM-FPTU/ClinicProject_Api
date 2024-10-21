using System;
using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Doctors.GetUsersHaveMedicalReport;

/// <summary>
///     GetUsersHaveMedicalReport Response
/// </summary>
public class GetUsersHaveMedicalReportResponse : IFeatureResponse
{
    public GetUsersHaveMedicalReportResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<User> Users { get; init; }

        public sealed class User
        {
            public Guid Id { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public GenderDTO Gender { get; init; }

            public sealed class GenderDTO
            {
                public Guid Id { get; init; }
                public string Name { get; init; }
                public string Constant { get; init; }
            }

            public int Age { get; init; }

            public string Address { get; init; }

        }
    }
}
