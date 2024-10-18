using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using System;
using System.Collections.Generic;
namespace Clinic.Application.Features.Admin.GetAllDoctor;

/// <summary>
///     GetAllDoctors Response
/// </summary>
public class GetAllDoctorResponse : IFeatureResponse
{
    public GetAllDoctorResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<User> Users { get; init; }

        public sealed class User
        {
            public Guid Id { get; init; }
            public string Username { get; init; }

            public string PhoneNumber { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public GenderDTO Gender { get; init; }
            public sealed class GenderDTO
            {
                public Guid Id { get; init; }
                public string Name { get; init; }
                public string Constant { get; init; }
            }

            public DateTime? DOB { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }

            public string Achievement { get; init; }

            public IEnumerable<SpecialtyDTO> Specialty { get; init; }
            public sealed class SpecialtyDTO
            {
                public Guid Id { get; init; }
                public string Name { get; init; }
                public string Constant { get; init; }

            }
            public PositionDTO Position { get; init; }
            public sealed class PositionDTO
            {
                public Guid Id { get; init; }
                public string Name { get; init; }
                public string Constant { get; init; }
            }
        }
    }
}

