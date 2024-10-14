using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.ExaminationServices.HiddenService;

/// <summary>
///     HiddenService Request
/// </summary>

public class HiddenServiceRequest : IFeatureRequest<HiddenServiceResponse>
{
    [BindFrom("serviceId")]
    public Guid ServiceId { get; set; }
}
