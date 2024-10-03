using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public class RemoveScheduleResponse : IFeatureResponse
{
    public RemoveScheduleResponseStatusCode StatusCode { get; init; }
}
