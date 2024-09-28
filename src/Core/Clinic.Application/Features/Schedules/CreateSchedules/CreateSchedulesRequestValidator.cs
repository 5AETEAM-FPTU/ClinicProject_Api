using System.Collections.Generic;
using System.Linq;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.CreateSchedules;

/// <summary>
///     CreateSchedules request validator.
/// </summary>
public sealed class CreateSchedulesRequestValidator
    : FeatureRequestValidator<CreateSchedulesRequest, CreateSchedulesResponse>
{
    public CreateSchedulesRequestValidator()
    {
        RuleFor(expression: request => request.TimeSlots).NotEmpty();

        RuleForEach(expression: request => request.TimeSlots)
            .ChildRules(timeslot =>
            {
                timeslot.RuleFor(slot => slot.StartTime).LessThan(slot => slot.EndTime);
            });

        RuleFor(expression: request => request.TimeSlots)
            .Must(predicate: NoOverlappingSlots)
            .WithMessage(errorMessage: "Overlapping time slots");
    }

    private bool NoOverlappingSlots(IEnumerable<CreateSchedulesRequest.TimeSlot> timeSlots)
    {
        var sortedTimeSlots = timeSlots
            .OrderBy(keySelector: timeSlot => timeSlot.StartTime)
            .ToList();

        for (int i = 1; i < sortedTimeSlots.Count; i++)
        {
            if (sortedTimeSlots[i].StartTime < sortedTimeSlots[i - 1].EndTime)
            {
                return false;
            }
        }
        return true;
    }
}
