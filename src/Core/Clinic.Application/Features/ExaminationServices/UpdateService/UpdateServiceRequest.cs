using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using System;

namespace Clinic.Application.Features.ExaminationServices.UpdateService;

/// <summary>
///     UpdateService Request
/// </summary>
public class UpdateServiceRequest : IFeatureRequest<UpdateServiceResponse>
{
    [BindFrom("serviceId")]
    public Guid ServiceId { get; set; }
    public string Code { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int? Price { get; init; }
    public string Group { get; init; }
}
