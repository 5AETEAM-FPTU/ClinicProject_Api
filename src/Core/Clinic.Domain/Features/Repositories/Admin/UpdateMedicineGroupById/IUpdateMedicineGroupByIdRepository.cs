using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.UpdateMedicineGroupById;

public interface IUpdateMedicineGroupByIdRepository
{
    Task<bool> IsMedicineGroupFound(Guid medicineGroupId, CancellationToken cancellationToken);
    Task<bool> UpdateMedicineGroupByIdQueryAsync(
        Guid medicineGroupId,
        string name,
        string constant,
        CancellationToken cancellationToken);
}
