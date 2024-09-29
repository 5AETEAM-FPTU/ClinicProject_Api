using System;
using System.Collections.Generic;
using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate Response Status Code
/// </summary>
public class GetSchedulesByDateResponse : IFeatureResponse
{
    public GetSchedulesByDateResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public List<TimeSlot> TimeSlots { get; init; }

        public sealed class TimeSlot
        {
            public DateTime StartTime { get; set; }

            public DateTime EndTime { get; set; }
        }
    }
}
