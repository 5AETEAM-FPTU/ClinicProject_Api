using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;
using FastEndpoints;

namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate Request
/// </summary>

public class GetSchedulesByDateRequest : IFeatureRequest<GetSchedulesByDateResponse>
{
    [BindFrom("date")]
    public DateTime Date { get; set; }
}
