using Clinic.Application.Commons.Abstractions;
using System.Collections.Generic;
using System;

namespace Clinic.Application.Features.Enums.GetAllPosition;

/// <summary>
///    GetAllPositionResponse
/// </summary>
public class GetAllPositionResponse : IFeatureResponse
{
    public GetAllPositionResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Position> Positions { get; init; }

        public sealed class Position
        {
            public Guid Id { get; init; }
            public string PositionName { get; init; }

            public string Constant { get; init; }
        }
    }
}
