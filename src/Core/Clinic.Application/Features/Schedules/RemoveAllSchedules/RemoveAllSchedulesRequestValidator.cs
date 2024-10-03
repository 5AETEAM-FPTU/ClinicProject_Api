
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.RemoveAllSchedules;

/// <summary>
///     CreateSchedules request validator.
/// </summary>
public sealed class RemoveAllSchedulesRequestValidator
    : FeatureRequestValidator<RemoveAllSchedulesRequest, RemoveAllSchedulesResponse>
{
    public RemoveAllSchedulesRequestValidator()
    {
        RuleFor(expression: request => request.Date).NotEmpty();
    }
}
