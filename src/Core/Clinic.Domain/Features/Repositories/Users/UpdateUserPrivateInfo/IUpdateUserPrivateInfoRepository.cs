using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateUserPrivateInfo;

public interface IUpdateUserPrivateInfoRepository
{
    Task<bool> UpdateUserPrivateInfoByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

}
