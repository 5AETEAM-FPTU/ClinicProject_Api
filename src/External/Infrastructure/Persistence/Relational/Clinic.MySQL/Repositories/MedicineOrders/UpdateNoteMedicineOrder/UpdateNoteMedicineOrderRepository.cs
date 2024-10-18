using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicineOrders.UpdateNoteMedicineOrder;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using static Clinic.Application.Features.MedicineOrders.GetMedicineOrderItems.GetMedicineOrderItemsResponse.Body.MedicineOrderDetail;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Clinic.MySQL.Repositories.MedicineOrders.UpdateNoteMedicineOrder;

public class UpdateNoteMedicineOrderRepository : IUpdateNoteMedicineOrderRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineOrder> _medicineOrders;

    public UpdateNoteMedicineOrderRepository(ClinicContext context)
    {
        _context = context;
        _medicineOrders = _context.Set<MedicineOrder>();
    }

    public Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken cancellationToken)
    {
        return _medicineOrders.AnyAsync(entity => entity.Id == medicineOrderId, cancellationToken);
    }

    public async Task<bool> UpdateNoteMedicineOrderCommandAsync(Guid medicineOrderId, string note, CancellationToken cancellationToken)
    {
        var existingMedicineOrder = await _medicineOrders.Where(entity =>
                entity.Id == medicineOrderId
        )
            .FirstOrDefaultAsync(cancellationToken);

        existingMedicineOrder.Note = note;

        _medicineOrders.Update(existingMedicineOrder);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

}
