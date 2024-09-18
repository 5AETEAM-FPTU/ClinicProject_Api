using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;

public interface IUpdateDoctorAchievementRepository
{
    Task<bool> UpdateDoctorAchievementByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken);
}
