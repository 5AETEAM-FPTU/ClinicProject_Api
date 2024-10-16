using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicineOrders.UpdateOrderItems;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.MedicineOrders.UpdateOrderItems;

public class UpdateMedicineOrderItemRepository : IUpdateMedicineOrderItemRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineOrder> _medicineOrders;
    private readonly DbSet<MedicineOrderItem> _medicineOrderItems;
    public UpdateMedicineOrderItemRepository(ClinicContext context)
    {
        _context = context;
        _medicineOrderItems = _context.Set<MedicineOrderItem>();
        _medicineOrders = _context.Set<MedicineOrder>();
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

    public async Task<bool> UpdateMedicineOrderItemCommandAsync(Guid medicineOrderId, Guid medicineId, int quantity, string description, CancellationToken cancellationToken)
    {
        var existingMedicineOrderItem = await _medicineOrderItems.Where( entity => 
                entity.MedicineOrderId == medicineOrderId
                && entity.MedicineId == medicineId
            )
            .FirstOrDefaultAsync(cancellationToken);

        existingMedicineOrderItem.Quantity = quantity;
        existingMedicineOrderItem.Description = description;

        _medicineOrderItems.Update(existingMedicineOrderItem);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
