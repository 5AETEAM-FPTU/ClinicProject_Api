using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Schedules.RemoveAllSchedules;

public interface IRemoveAllSchedulesRepository
{
    Task<bool> RemoveAllSchedulesByDateCommandAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken);
}
