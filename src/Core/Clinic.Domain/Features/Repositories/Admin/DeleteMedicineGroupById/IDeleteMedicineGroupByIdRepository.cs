using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.DeleteMedicineGroupById;

public interface IDeleteMedicineGroupByIdRepository
{
    Task<bool> IsMedicineGroupExist(Guid medicineGroupId, CancellationToken cancellationToken);
    Task<bool> DeleteMedicineGroupByIdCommandAsync(Guid medicineGroupId, CancellationToken cancellationToken);
}
