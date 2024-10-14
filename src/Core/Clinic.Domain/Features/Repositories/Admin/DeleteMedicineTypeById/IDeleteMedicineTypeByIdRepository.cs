using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.DeleteMedicineTypeById;

public interface IDeleteMedicineTypeByIdRepository
{
    Task<bool> IsMedicineTypeExist(Guid medicineTypeId, CancellationToken cancellationToken);
    Task<bool> DeleteMedicineTypeByIdCommandAsync(Guid medicineTypeId, CancellationToken cancellationToken);
}

