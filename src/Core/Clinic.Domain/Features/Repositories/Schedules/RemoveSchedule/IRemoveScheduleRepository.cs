using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Schedules.RemoveSchedule;

public interface IRemoveScheduleRepository
{
    Task<bool> IsScheduleExist(Guid doctorId, Guid scheduleId);
    Task<bool> IsScheduleHadAppointment(Guid doctorId, Guid scheduleId, CancellationToken cancellationToken);
    Task<bool> RemoveScheduleByIdCommandAsync(Guid doctorId, Guid scheduleId, CancellationToken cancellationToken);
}
