using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.MedicineOrders.RemoveOrderItems;

public interface IRemoveMedicineOrderItemRepository
{
    Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken);
    Task<bool> UpdateMedicineOrderItemCommandAsync(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken);
    Task<bool> IsMedicineAlreadyExist(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken);

}
