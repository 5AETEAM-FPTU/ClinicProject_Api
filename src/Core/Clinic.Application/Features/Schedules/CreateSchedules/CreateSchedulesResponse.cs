using Clinic.Application.Commons.Abstractions;

namespace Clinic.Application.Features.Schedules.CreateSchedules;

/// <summary>
///     CreateSchedules Response Status Code
/// </summary>
public class CreateSchedulesResponse : IFeatureResponse
{
    public CreateSchedulesResponseStatusCode StatusCode { get; init; }
}
