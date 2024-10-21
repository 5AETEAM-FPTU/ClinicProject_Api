using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Schedules.UpdateSchedule;

public interface IUpdateScheduleByIdRepository
{
    Task<bool> AreOverLappedSchedule(Guid doctorId, Guid scheduleId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    Task<bool> IsScheduleExist(Guid scheduleId);
    Task<bool> UpdateScheduleByIdCommandAsync(Guid scheduleId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}
