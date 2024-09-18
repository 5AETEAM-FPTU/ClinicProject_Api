using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.GetProfileDoctor;

public interface IGetProfileDoctorRepository
{
    Task<User> GetDoctorByDoctorIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
