
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Clinic.Domain.Features.Repositories.MedicineOrders.UpdateNoteMedicineOrder;

public interface IUpdateNoteMedicineOrderRepository
{
    Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken);
    Task<bool> UpdateNoteMedicineOrderCommandAsync(Guid medicineOrderId, string note, CancellationToken cancellationToken);
}
