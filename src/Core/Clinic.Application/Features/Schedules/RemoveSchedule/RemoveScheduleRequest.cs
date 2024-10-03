using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules Request
/// </summary>

public class RemoveScheduleRequest : IFeatureRequest<RemoveScheduleResponse>
{
    [BindFrom("scheduleId")]
    public Guid ScheduleId { get; set; } 
}
