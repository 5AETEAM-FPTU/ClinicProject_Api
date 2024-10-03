using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Schedules.GetSchedulesByDate;

/// <summary>
///     Interface for Query GetSchedulesByDate Repository
/// </summary>
public interface IGetSchedulesByDateRepository
{
    Task<IEnumerable<Schedule>> GetSchedulesByDateQueryAsync(
        Guid doctorId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default
    );
}
