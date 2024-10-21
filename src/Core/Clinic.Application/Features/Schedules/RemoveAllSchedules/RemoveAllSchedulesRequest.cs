using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Schedules.RemoveAllSchedules;

/// <summary>
///     CreateSchedules Request
/// </summary>

public class RemoveAllSchedulesRequest : IFeatureRequest<RemoveAllSchedulesResponse>
{
    [QueryParam ,BindFrom("date")]
    public DateTime Date { get; set; }

    [QueryParam, BindFrom("DoctorId")]
    public Guid? DoctorId { get; set; }
}
