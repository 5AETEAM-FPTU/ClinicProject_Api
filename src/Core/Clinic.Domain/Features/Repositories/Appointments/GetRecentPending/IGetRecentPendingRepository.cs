using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.GetRecentPending;

/// <summary>
///     Interface IGetRecentPendingRepository
/// </summary>
public interface IGetRecentPendingRepository
{
    Task<IEnumerable<Appointment>> FindRecentPendingQueryAsync(CancellationToken cancellationToken);
}
