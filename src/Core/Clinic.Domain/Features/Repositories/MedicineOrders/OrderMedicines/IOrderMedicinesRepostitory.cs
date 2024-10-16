using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.MedicineOrders.OrderMedicines;

public interface IOrderMedicinesRepostitory
{
    Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken);
    Task<bool> IsMedicineAvailable(Guid medicineId, CancellationToken cancellationToken);
    Task<bool> AddMedicineOrderItemCommandAsync(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken);
    Task<bool> IsMedicineAlreadyExist(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken);

}
