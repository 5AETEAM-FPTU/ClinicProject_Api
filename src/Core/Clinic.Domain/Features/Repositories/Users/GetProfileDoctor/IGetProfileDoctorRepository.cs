using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.GetProfileDoctor;

public interface IGetProfileDoctorRepository
{
    Task<User> GetProfileDoctorByDoctorIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
