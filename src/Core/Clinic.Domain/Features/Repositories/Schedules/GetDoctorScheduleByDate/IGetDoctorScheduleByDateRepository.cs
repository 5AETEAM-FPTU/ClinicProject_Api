using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Schedules.GetDoctorScheduleByDate;

public interface IGetDoctorScheduleByDateRepository
{
    Task<User> GetUserByDoctorId(Guid doctorId, CancellationToken cancellationToken);
    Task<IEnumerable<Schedule>> GetSchedulesByDateQueryAsync(
        Guid doctorId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default
    );
}
