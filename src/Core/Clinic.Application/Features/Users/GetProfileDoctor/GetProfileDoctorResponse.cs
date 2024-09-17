using Clinic.Application.Commons.Abstractions.GetProfileUser;
using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.Users.GetProfileDoctor;

/// <summary>
///     GetProfileDoctor Response
/// </summary>
public class GetProfileDoctorResponse : IFeatureResponse
{
    public GetProfileDoctorResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public UserDetail User { get; init; }

        public sealed class UserDetail
        {
            public string Username { get; init; }

            public string PhoneNumber { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public string Gender { get; init; }

            public DateTime DOB { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }

            public string Achievement { get; init; }

            public string Specialty { get; init; }
            public string Position { get; init; }


        }
    }
}
