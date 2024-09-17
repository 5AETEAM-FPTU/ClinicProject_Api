using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.GetProfileUser;

public interface IGetProfileUserRepository
{
    Task<User> GetProfileUserByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}
