using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Schedules.GetDoctorMonthlyDate;

public interface IGetDoctorMonthlyDateRepository
{
    Task<User> GetUserByDoctorId(Guid doctorId, CancellationToken cancellationToken);
    Task<IEnumerable<Schedule>> GetScheduleDatesByMonthQueryAsync(
        int year,
        int month,
        Guid doctorId,
        CancellationToken cancellationToken = default
    );
}
