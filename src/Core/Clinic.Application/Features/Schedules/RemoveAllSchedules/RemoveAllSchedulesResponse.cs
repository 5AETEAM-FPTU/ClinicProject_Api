using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.RemoveAllSchedules;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public class RemoveAllSchedulesResponse : IFeatureResponse
{
    public RemoveAllSchedulesResponseStatusCode StatusCode { get; init; }
}
