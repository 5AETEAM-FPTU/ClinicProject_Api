using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ExaminationServices.RemoveService;

/// <summary>
///     RemoveService Request
/// </summary>

public class RemoveServiceRequest : IFeatureRequest<RemoveServiceResponse>
{
    [BindFrom("serviceId")]
    public Guid ServiceId { get; set; }
}
