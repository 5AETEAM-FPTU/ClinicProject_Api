using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Schedules.GetScheduleDatesByMonth;

public interface IGetScheduleDatesByMonthRepository
{
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<Schedule>> GetScheduleDatesByMonthQueryAsync(
        int year,
        int month,
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
