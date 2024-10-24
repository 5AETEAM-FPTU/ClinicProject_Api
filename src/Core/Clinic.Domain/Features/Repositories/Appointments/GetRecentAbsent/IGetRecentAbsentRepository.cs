using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.GetRecentAbsent;

/// <summary>
///     Interface IGetRecentAbsentRepository
/// </summary>
public interface IGetRecentAbsentRepository
{
    Task<IEnumerable<Appointment>> FindRecentAbsentQueryAsync(CancellationToken cancellationToken);
}
