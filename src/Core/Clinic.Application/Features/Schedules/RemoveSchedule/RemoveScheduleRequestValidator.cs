using System.Collections.Generic;
using System.Linq;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.RemoveSchedule;

/// <summary>
///     CreateSchedules request validator.
/// </summary>
public sealed class RemoveScheduleRequestValidator
    : FeatureRequestValidator<RemoveScheduleRequest, RemoveScheduleResponse>
{
    public RemoveScheduleRequestValidator()
    {
    }
}
