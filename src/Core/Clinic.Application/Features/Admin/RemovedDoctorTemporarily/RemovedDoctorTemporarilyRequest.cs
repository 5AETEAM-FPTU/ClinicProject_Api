using System;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Admin.RemovedDoctorTemporarily;

/// <summary>
///     RemovedDoctorTemporarilyl Request
/// </summary>

public class RemovedDoctorTemporarilyRequest : IFeatureRequest<RemovedDoctorTemporarilyResponse>
{
    [BindFrom("doctorId")]
    public Guid DoctorId { get; set; }
}
