
using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Enums.GetAllGender;

/// <summary>
///     GetAllGenderResponse
/// </summary>
public class GetAllGenderResponse : IFeatureResponse
{
    public GetAllGenderResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Gender> Genders { get; init; }

        public sealed class Gender
        {
            public Guid Id { get; init; }
            public string GenderName { get; init; }

            public string Constant { get; init; }
        }
    }
}
