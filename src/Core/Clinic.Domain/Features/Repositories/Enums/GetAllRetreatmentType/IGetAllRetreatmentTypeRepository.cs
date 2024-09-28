using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Enums.GetAllRetreatmentType;

/// <summary>
///     Interface for Query GetAllRetreatmentType Repository
/// </summary>
public interface IGetAllRetreatmentTypeRepository
{
    Task<IEnumerable<RetreatmentType>> FindAllRetreatmentTypeQueryAsync(CancellationToken cancellationToken);
}
