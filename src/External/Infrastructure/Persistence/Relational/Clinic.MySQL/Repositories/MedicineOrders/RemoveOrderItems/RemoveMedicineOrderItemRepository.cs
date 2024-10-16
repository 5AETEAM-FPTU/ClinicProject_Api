using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicineOrders.RemoveOrderItems;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.MedicineOrders.RemoveOrderItems;

public class RemoveMedicineOrderItemRepository : IRemoveMedicineOrderItemRepository
{
    public ClinicContext _context;
    public DbSet<MedicineOrder> _medicineOrders;
    public DbSet<MedicineOrderItem> _medicineOrderItems;
    public RemoveMedicineOrderItemRepository(ClinicContext context)
    {
        _context = context;
        _medicineOrders = _context.Set<MedicineOrder>();
        _medicineOrderItems = _context.Set<MedicineOrderItem>();
    }

    public Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken)
    {
        return _medicineOrders.AnyAsync(entity => entity.Id == medicineOrderId, cancellationToken);
    }

    public Task<bool> IsMedicineAlreadyExist(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken)
    {
        return _medicineOrderItems
            .Where(entity => entity.MedicineOrderId == medicineOrderId)
            .AnyAsync(entity => entity.MedicineId == medicineId);
    }

    public async Task<bool> UpdateMedicineOrderItemCommandAsync(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken)
    {
        var existingMedicineOrderItem = await _medicineOrderItems.Where(entity =>
                entity.MedicineOrderId == medicineOrderId
                && entity.MedicineId == medicineId
            )
            .FirstOrDefaultAsync(cancellationToken);

        _medicineOrderItems.Remove(existingMedicineOrderItem);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
