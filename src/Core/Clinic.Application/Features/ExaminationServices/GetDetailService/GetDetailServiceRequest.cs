using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.ExaminationServices.GetDetailService;

public class GetDetailServiceRequest : IFeatureRequest<GetDetailServiceResponse>
{
    [BindFrom("serviceId")]
    public Guid ServiceId { get; init; }
}
