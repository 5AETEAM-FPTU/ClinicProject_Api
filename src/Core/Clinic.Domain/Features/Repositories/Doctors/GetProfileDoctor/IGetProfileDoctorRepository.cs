using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.GetProfileDoctor;

public interface IGetProfileDoctorRepository
{
    Task<User> GetDoctorByDoctorIdQueryAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
}
