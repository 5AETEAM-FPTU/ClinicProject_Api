using Clinic.Domain.Commons.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Clinic.Domain.Features.Repositories.Admin.GetAllMedicineType;

/// <summary>
///     Interface for Query GetAllMedicineType Repository
/// </summary>
public interface IGetAllMedicineTypeRepository
{
    Task<IEnumerable<MedicineType>> FindAllMedicineTypeQueryAsync(CancellationToken cancellationToken);
}
