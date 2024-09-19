using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateUserDescription;

public interface IUpdateUserDescriptionRepository
{
    Task<bool> UpdateUserDescriptionByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}
