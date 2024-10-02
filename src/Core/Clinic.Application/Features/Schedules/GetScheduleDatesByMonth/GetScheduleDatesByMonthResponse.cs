using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;

/// <summary>
///     GetSchedulesByDate Response Status Code
/// </summary>
public class GetScheduleDatesByMonthResponse : IFeatureResponse
{
    public GetScheduleDatesByMonthResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public List<DateTime> WorkingDays { get; set; }
    }
}
