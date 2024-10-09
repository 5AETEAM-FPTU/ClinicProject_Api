using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Doctors.UpdateDutyStatus;

public interface IUpdateDutyStatusRepository
{
    Task<User> GetDoctorById(Guid id);
    Task<bool> UpdateDutyStatusCommandAsync(
        User doctor, bool status, CancellationToken cancellationToken
    );
}
