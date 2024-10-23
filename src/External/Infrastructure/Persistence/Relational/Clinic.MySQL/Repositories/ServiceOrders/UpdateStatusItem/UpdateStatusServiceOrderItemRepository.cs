using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ServiceOrders.UpdateStatusItem;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;


public class UpdateStatusServiceOrderItemRepository : IUpdateStatusServiceOrderItemRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<ServiceOrderItem> _serviceOrderItems;

    public UpdateStatusServiceOrderItemRepository(ClinicContext context)
    {
        _context = context;
        _serviceOrderItems = _context.Set<ServiceOrderItem>();
    }

    public async Task<bool> IsServiceItemExist(Guid serviceOrderId, Guid serviceId)
    {
        return await _serviceOrderItems.AnyAsync(entity => entity.ServiceOrderId == serviceOrderId && entity.ServiceId == serviceId);
    }

    public async Task<bool> UpdateStatusServiceOrderItemCommandAsync(Guid serviceOrderId, Guid serviceId, CancellationToken cancellationToken)
    {
        var existingItem = await _serviceOrderItems.FirstOrDefaultAsync(
                entity => entity.ServiceOrderId == serviceOrderId && entity.ServiceId == serviceId
            );
        try
        {
            existingItem.IsUpdated = true;
            _serviceOrderItems.Update(existingItem);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
