using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.GetAllMedicineGroup;

/// <summary>
///     Interface for Query GetAllMedicineGroup Repository
/// </summary>
public interface IGetAllMedicineGroupRepository
{
    Task<IEnumerable<MedicineGroup>> FindAllMedicineGroupQueryAsync(CancellationToken cancellationToken);
}
