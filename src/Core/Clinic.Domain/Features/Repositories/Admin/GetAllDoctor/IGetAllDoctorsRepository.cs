
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetAllDoctor;

/// <summary>
///     Interface for Query GetAllUsersRepository Repository
/// </summary>
public interface IGetAllDoctorsRepository
{
    Task<IEnumerable<User>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken);
}
