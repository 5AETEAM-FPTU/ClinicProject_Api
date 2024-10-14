using Clinic.Application.Commons.Abstractions;
using System;

namespace Clinic.Application.Features.ExaminationServices.GetDetailService;

/// <summary>
///     GetMedicineById Response
/// </summary>
public class GetDetailServiceResponse : IFeatureResponse
{
    public GetDetailServiceResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public ServiceDetail Service { get; init; }

        public sealed class ServiceDetail
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

