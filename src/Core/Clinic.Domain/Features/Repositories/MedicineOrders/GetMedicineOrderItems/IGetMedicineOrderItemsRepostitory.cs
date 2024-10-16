using Clinic.Domain.Commons.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.MedicineOrders.GetMedicineOrderItems;

public interface IGetMedicineOrderItemsRepostitory
{
    Task<MedicineOrder> GetMedicineOrderItemsQueryAsync(Guid medicineOrderId, CancellationToken cancellationToken);
    Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken CancellationToken);
}
