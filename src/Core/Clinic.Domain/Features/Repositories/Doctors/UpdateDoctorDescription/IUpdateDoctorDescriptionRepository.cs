using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Doctors.UpdateDoctorDescription;

public interface IUpdateDoctorDescriptionRepository
{
    Task<bool> UpdateDoctorDescriptionByIdCommandAsync(
        User user,
        CancellationToken cancellationToken
    );

    public Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken);
}
