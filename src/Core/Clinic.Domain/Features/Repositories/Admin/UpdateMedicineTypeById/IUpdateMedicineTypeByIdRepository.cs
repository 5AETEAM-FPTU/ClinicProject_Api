using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.UpdateMedicineTypeById;

public interface IUpdateMedicineTypeByIdRepository
{
    Task<bool> IsMedicineTypeFound(Guid medicineTypeId, CancellationToken cancellationToken);
    Task<bool> UpdateMedicineTypeByIdQueryAsync(
        Guid medicineTypeId,
        string name,
        string constant,
        CancellationToken cancellationToken);
}
