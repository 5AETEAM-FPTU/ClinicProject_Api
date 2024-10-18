using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.Admin.GetMedicineGroupById;

/// <summary>
///     Interface for Query GetMedicineGroupById Repository
/// </summary>
public interface IGetMedicineGroupByIdRepository
{
    Task<MedicineGroup> FindMedicineGroupByIdQueryAsync(
        Guid medicineGroupId,
        CancellationToken cancellationToken
    );
}
