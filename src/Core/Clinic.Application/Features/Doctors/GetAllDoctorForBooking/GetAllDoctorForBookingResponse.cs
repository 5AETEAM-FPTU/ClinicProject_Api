using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;
using Clinic.Application.Commons.Pagination;

namespace Clinic.Application.Features.Doctors.GetAllDoctorForBooking;

/// <summary>
///     GetAllDoctorForBookingResponse
/// </summary>
public class GetAllDoctorForBookingResponse : IFeatureResponse
{
    public GetAllDoctorForBookingResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<UserDetail> userDetails { get; init; }

        public sealed class UserDetail
        {
            public Guid Id { get; init; }
            public string Username { get; init; }

            public string PhoneNumber { get; init; }

            public string AvatarUrl { get; init; }

            public string FullName { get; init; }

            public ResponseGender Gender { get; init; }

            public sealed class ResponseGender
            {
                public Guid Id { get; init; }
                public string GenderName { get; init; }
            }

            public DateTime? DOB { get; init; }

            public string Address { get; init; }

            public string Description { get; init; }

            public string Achievement { get; init; }

            public IEnumerable<ResponseSpecialties> Specialties { get; init; }

            public sealed class ResponseSpecialties
            {
                public Guid Id { get; init; }
                public string SpecialtyName { get; init; }
            }

            public ResponsePosition Position { get; init; }

            public sealed class ResponsePosition
            {
                public Guid Id { get; init; }
                public string PositionName { get; init; }
            }
            public double Rating { get; init; }
        }
    }
}