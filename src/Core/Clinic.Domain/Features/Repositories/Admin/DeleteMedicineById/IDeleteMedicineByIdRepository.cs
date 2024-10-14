using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.Admin.DeleteMedicineById;

public interface IDeleteMedicineByIdRepository
{
    Task<bool> IsMedicineExist(Guid medicineId, CancellationToken cancellationToken);
    Task<bool> DeleteMedicineByIdCommandAsync(Guid medicineId, CancellationToken cancellationToken);
}
