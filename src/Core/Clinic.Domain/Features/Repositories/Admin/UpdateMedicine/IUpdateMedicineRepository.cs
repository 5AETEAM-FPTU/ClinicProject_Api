using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Admin.UpdateMedicine;

public interface IUpdateMedicineRepository
{
    Task<bool> IsMedicineTypeFound(Guid medicineTypeId, CancellationToken cancellationToken);
    Task<bool> IsMedicineGroupFound(Guid medicineGroupId, CancellationToken cancellationToken);
    Task<bool> UpdateMedicineQueryAsync(
        Guid medicineId,
        string medicineName,
        string manufacture,
        Guid medicineGroupId,
        string ingredient,
        Guid medicineTypeId,
        CancellationToken cancellationToken);
    Task<bool> IsMedicineFoundById(Guid medicineId, CancellationToken cancellationToken);
}
