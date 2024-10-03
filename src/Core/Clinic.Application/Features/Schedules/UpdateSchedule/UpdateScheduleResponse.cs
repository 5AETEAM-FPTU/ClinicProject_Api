using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.UpdateSchedule;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public class UpdateScheduleResponse : IFeatureResponse
{
    public UpdateScheduleResponseStatusCode StatusCode { get; init; }
}
