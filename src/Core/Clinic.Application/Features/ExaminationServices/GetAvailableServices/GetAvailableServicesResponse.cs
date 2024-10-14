using Clinic.Application.Commons.Abstractions;
using System;
using System.Collections.Generic;

namespace Clinic.Application.Features.ExaminationServices.GetAvailableServices;

/// <summary>
///     GetAvailableServices Response
/// </summary>
public class GetAvailableServicesResponse : IFeatureResponse
{
    public GetAvailableServicesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Service> Services { get; init; }

        public sealed class Service
        {
            public Guid Id { get; init; }
            public string Name { get; init; }
            public string Code { get; init; }
            public int Price { get; init; }
            public string Group { get; init; }
            public string Description { get; init; }
        }
    }
}
