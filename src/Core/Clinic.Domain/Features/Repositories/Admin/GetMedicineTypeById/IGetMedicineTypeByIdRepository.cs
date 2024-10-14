using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetMedicineTypeById;

/// <summary>
///     Interface for Query GetMedicineTypeById Repository
/// </summary>
public interface IGetMedicineTypeByIdRepository
{
    Task<MedicineType> FindMedicineTypeByIdQueryAsync(
        Guid medicineTypeId,
        CancellationToken cancellationToken
    );
}
