using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Doctors.GetUserInforById;

/// <summary>
///     GetUserInforById Response
/// </summary>
public class GetUserInforByIdResponse : IFeatureResponse
{
    public GetUserInforByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public UserDetail User { get; init; }

        public sealed class UserDetail
        {
            public Guid UserId { get; init; }
            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public ResponseGender Gender { get; init; }

            public sealed class ResponseGender
            {
                public Guid Id { get; init; }
                public string GenderName { get; init; }
            }

            public int Age { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }
        }
    }
}
