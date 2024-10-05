﻿using System.Collections.Generic;
using System.Linq;
using Clinic.Application.Commons.Abstractions;
using FluentValidation;

namespace Clinic.Application.Features.Schedules.UpdateSchedule;

/// <summary>
///     CreateSchedules request validator.
/// </summary>
public sealed class UpdateScheduleRequestValidator
    : FeatureRequestValidator<UpdateScheduleRequest, UpdateScheduleResponse>
{
    public UpdateScheduleRequestValidator()
    {
        RuleFor(expression: request => request.StartDate < request.EndDate);
    }
}