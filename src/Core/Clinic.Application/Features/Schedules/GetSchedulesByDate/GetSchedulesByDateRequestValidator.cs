using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.GetSchedulesByDate;

/// <summary>
///     GetSchedulesByDate request validator.
/// </summary>
public sealed class GetSchedulesByDateRequestValidator
    : FeatureRequestValidator<GetSchedulesByDateRequest, GetSchedulesByDateResponse>
{
    public GetSchedulesByDateRequestValidator()
    {
        RuleFor(expression: request => request.Date).NotEmpty();
    }
}
