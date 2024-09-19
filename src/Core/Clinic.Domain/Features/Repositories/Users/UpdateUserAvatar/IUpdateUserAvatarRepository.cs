using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateUserAvatar;

public interface IUpdateUserAvatarRepository
{
    Task<bool> UpdateUserAvatarByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
}
