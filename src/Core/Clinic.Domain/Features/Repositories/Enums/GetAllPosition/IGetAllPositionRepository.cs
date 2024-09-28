using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllPosition;


/// <summary>
///     Interface for Query GetAllPosition Repository
/// </summary>
public interface IGetAllPositionRepository
{
    Task<IEnumerable<Position>> FindAllPositionQueryAsync(CancellationToken cancellationToken);
}

