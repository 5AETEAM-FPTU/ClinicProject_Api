
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetAllUser;

public interface IGetAllUsersRepository
{
    Task<IEnumerable<User>> FindUserByIdQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountAllUserQueryAsync(CancellationToken cancellationToken);
}
