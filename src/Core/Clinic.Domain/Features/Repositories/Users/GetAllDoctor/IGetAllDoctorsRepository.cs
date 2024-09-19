using Clinic.Domain.Commons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.GetAllDoctor;

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

