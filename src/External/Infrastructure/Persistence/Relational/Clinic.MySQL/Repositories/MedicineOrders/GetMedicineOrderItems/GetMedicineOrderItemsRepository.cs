using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.MedicineOrders.GetMedicineOrderItems;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.MedicineOrders.GetMedicineOrderItems;

public class GetMedicineOrderItemsRepository : IGetMedicineOrderItemsRepostitory
{
    private readonly ClinicContext _context;
    private readonly DbSet<MedicineOrder> _medicineOrders;

    public GetMedicineOrderItemsRepository(ClinicContext context)
    {
        _context = context;
        _medicineOrders = _context.Set<MedicineOrder>();
    }

    public async Task<MedicineOrder> GetMedicineOrderItemsQueryAsync(Guid medicineOrderId, CancellationToken cancellationToken)
    {
        var serviceOrder = await _medicineOrders
            .AsNoTracking()
            .Where(entity => entity.Id == medicineOrderId)
            .Select(entity =>
                new MedicineOrder
                {
                    Id = entity.Id,
                    MedicineOrderItems = entity.MedicineOrderItems.Select(item => new MedicineOrderItem
                    {
                        MedicineId = item.MedicineId,
                        Quantity = item.Quantity,
                        Description = item.Description,
                        Medicine = new Medicine()
                        {
                            Id = item.Medicine.Id,
                            Name = item.Medicine.Name,
                            MedicineType = new MedicineType()
                            {
                                Id = item.Medicine.MedicineType.Id,
                                Name = item.Medicine.MedicineType.Name,
                                Constant = item.Medicine.MedicineType.Constant
                            }
                        }
                    })
                    .ToList()
                }
            )
            .FirstOrDefaultAsync(cancellationToken);

        return serviceOrder;
    }

    public async Task<bool> IsMedicineOrderExist(Guid medicineOrderId, CancellationToken CancellationToken)
    {
        return await _medicineOrders.AnyAsync(entity => entity.Id == medicineOrderId);
    }
}
