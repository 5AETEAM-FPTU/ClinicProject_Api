using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using System;
namespace Clinic.Application.Features.Users.GetAllUser;

/// <summary>
///     GetAllDoctors Response
/// </summary>
public class GetAllUserResponse : IFeatureResponse
{
    public GetAllUserResponseStatusCode StatusCode { get; init; }

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

            public string Gender { get; init; }

            public DateTime? DOB { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }

        }
    }
}

