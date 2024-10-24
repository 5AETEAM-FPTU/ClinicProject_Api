using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Appointments.GetAbsentForStaff;

/// <summary>
///     Interface IGetAbsentForStaffRepository
/// </summary>
public interface IGetAbsentForStaffRepository
{
    Task<IEnumerable<Appointment>> GetAbsentForStaffByUserIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );
}
