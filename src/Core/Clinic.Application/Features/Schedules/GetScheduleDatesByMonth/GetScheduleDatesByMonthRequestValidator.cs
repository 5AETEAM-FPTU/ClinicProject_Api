using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.GetScheduleDatesByMonth;

/// <summary>
///     GetSchedulesByDate request validator.
/// </summary>
public sealed class GetScheduleDatesByMonthRequestValidator
    : FeatureRequestValidator<GetScheduleDatesByMonthRequest, GetScheduleDatesByMonthResponse>
{
    public GetScheduleDatesByMonthRequestValidator()
    {
        RuleFor(expression: request => request.Year).NotEmpty();
        RuleFor(expression: request => request.Month)
            .NotEmpty()
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12."); ;
    }
}
