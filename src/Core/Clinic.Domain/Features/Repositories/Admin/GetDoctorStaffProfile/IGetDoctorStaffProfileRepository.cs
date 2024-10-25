using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetDoctorStaffProfile;

public interface IGetDoctorStaffProfileRepository
{
    Task<User> GetDoctorByDoctorIdQueryAsync(Guid userId, CancellationToken cancellationToken);
}
