using Clinic.Application.Commons.Abstractions;
using Clinic.Application.Commons.Pagination;
using System;

namespace Clinic.Application.Features.ExaminationServices.GetAllServices;

/// <summary>
///     GetAllServices Response
/// </summary>
public class GetAllServicesResponse : IFeatureResponse
{
    public GetAllServicesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Service> Services { get; init; }

        public sealed class Service
        {
            public Guid Id { get; init; }
            public string Name { get; init; }
            public string Code { get; init; }
            public int Price { get; init; }
            public string Group { get; init; }
            
        }
    }
}
