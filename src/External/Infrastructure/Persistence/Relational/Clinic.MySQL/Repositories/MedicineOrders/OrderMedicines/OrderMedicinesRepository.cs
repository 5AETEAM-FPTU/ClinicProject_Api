using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicineOrders.OrderMedicines;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.MedicineOrders.OrderMedicines;

public class OrderMedicinesRepository : IOrderMedicinesRepostitory
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineOrder> _medicineOrders;
    private readonly DbSet<MedicineOrderItem> _medicineOrderItems;
    private readonly DbSet<Medicine> _medicines;

    public OrderMedicinesRepository(ClinicContext context)
    {
        _context = context;
        _medicineOrders = _context.Set<MedicineOrder>();
        _medicineOrderItems = _context.Set<MedicineOrderItem>();
        _medicines = _context.Set<Medicine>();
    }

    public Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken)
    {
        return _medicineOrders.AnyAsync(entity => entity.Id == medicineOrderId, cancellationToken);
    }

    public Task<bool> IsMedicineAvailable(Guid medicineId, CancellationToken cancellationToken)
    {
        return _medicines.AnyAsync(entity => entity.Id == medicineId, cancellationToken);

    }

    public Task<bool> IsMedicineAlreadyExist(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken)
    {
        return _medicineOrderItems
            .Where(entity => entity.MedicineOrderId == medicineOrderId)
            .AnyAsync(entity => entity.MedicineId == medicineId);
    }

    public async Task<bool> AddMedicineOrderItemCommandAsync(Guid medicineOrderId, Guid medicineId, CancellationToken cancellationToken)
    {
        var medicineOrder = await _medicineOrders
            .Include(mo => mo.MedicineOrderItems)
            .FirstOrDefaultAsync(mo => mo.Id == medicineOrderId, cancellationToken);

        if (medicineOrder == null)
        {
            return false;
        }

        var medicine = await _medicines
            .FirstOrDefaultAsync(m => m.Id == medicineId, cancellationToken);

        if (medicine == null)
        {
            return false;
        }

        var newMedicineOrderItem = new MedicineOrderItem
        {
            MedicineOrderId = medicineOrder.Id,
            MedicineId = medicine.Id,
            Description = string.Empty,
            Quantity = 0
        };

        _medicineOrderItems.Add(newMedicineOrderItem);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
