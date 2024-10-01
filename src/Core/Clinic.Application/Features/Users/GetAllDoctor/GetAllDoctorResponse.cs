using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
namespace Clinic.Application.Features.Users.GetAllDoctor;

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

            public Gender Gender { get; init; }

            public DateTime? DOB { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }

            public string Achievement { get; init; }

            public IEnumerable<Specialty> Specialty { get; init; }

            public Position Position { get; init; }

        }
    }
}

