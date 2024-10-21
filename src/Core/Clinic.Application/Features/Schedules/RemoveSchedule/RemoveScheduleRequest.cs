using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules Request
/// </summary>

public class RemoveScheduleRequest : IFeatureRequest<RemoveScheduleResponse>
{
    [QueryParam, BindFrom("scheduleId")]
    public Guid ScheduleId { get; set; }

    [QueryParam, BindFrom("DoctorId")]
    public Guid? DoctorId { get; set; }
}
