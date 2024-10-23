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
        string keyword,
        CancellationToken cancellationToken
    );

    Task<int> CountAllDoctorsQueryAsync(string keyword, CancellationToken cancellationToken);
}
