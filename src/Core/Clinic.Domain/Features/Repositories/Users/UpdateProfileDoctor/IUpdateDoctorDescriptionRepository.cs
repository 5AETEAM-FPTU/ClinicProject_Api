using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;

public interface IUpdateDoctorDescriptionRepository
{
    Task<bool> UpdateDoctorDescriptionByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken);
    
}
