using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Enums.GetAllRetreatmentType;

/// <summary>
///    GetAllRetreatmentTypeResponse
/// </summary>
public class GetAllRetreatmentTypeResponse : IFeatureResponse
{
    public GetAllRetreatmentTypeResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<RetreamentType> RetreamentTypes { get; init; }

        public sealed class RetreamentType
        {
            public Guid Id { get; init; }
            public string TypeName { get; init; }

            public string Constant { get; init; }
        }
    }
}