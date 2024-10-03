using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Schedules.CreateSchedules;

/// <summary>
///     Interface for Query CreateSchedules Repository
/// </summary>
public interface ICreateSchedulesRepository
{
    Task<bool> CreateSchedulesAsync(
        IEnumerable<Schedule> createSchedules,
        CancellationToken cancellationToken = default
    );

    Task<bool> AreOverLappedSlotTimes(
        IEnumerable<Schedule> createSchedules,
        CancellationToken cancellationToken = default
    );
}
