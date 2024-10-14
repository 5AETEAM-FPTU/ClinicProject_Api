using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ServiceOrders.AddOrderService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using Clinic.Application.Commons.Constance;


namespace Clinic.MySQL.Repositories.ServiceOrders.AddOrderService;

public class AddOrderServiceRepository : IAddOrderServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<ServiceOrder> _serviceOrders;
    private readonly DbSet<ServiceOrderItem> _serviceOrderItems;
    private readonly DbSet<Service> _services;
    
    public AddOrderServiceRepository(ClinicContext context)
    {
        _context = context;
        _serviceOrders = _context.Set<ServiceOrder>();
        _serviceOrderItems = _context.Set<ServiceOrderItem>();
        _services = _context.Set<Service>();
    }

    public async Task<bool> AddServiceIntoServiceOrderCommandAsync(
        Guid serviceOrderId, 
        IEnumerable<Guid> serviceIds, 
        CancellationToken cancellationToken)
    {
        // Get ServiceOrder By Id.
        var serviceOrder = await _serviceOrders
            .Include(entity => entity.ServiceOrderItems)
            .FirstOrDefaultAsync(entity => entity.Id == serviceOrderId, cancellationToken);

        if (serviceOrder == null)
        {
            return false;
        }

        // Get current ServiceOrderItem of ServiceOrder.
        var existingServiceOrderItems = serviceOrder.ServiceOrderItems.ToList();
        var existingServiceIds = existingServiceOrderItems.Select(entity => entity.ServiceId).ToList();

        // Filter ServiceOrderItem need to remove
        var serviceOrderItemsToRemove = existingServiceOrderItems
            .Where(entity => !serviceIds.Contains(entity.ServiceId))
            .ToList();

        _serviceOrderItems.RemoveRange(serviceOrderItemsToRemove);

        // Filter ServiceOrderItem need to add
        var serviceOrderItemsToAdd = serviceIds
            .Where(id => !existingServiceIds.Contains(id))
            .Select(id => new ServiceOrderItem
            {
                ServiceOrderId = serviceOrderId,
                ServiceId = id,
                PriceAtOrder = (int)_services.FirstOrDefault(service => service.Id == id).Price,
            })
            .ToList();

        if (serviceOrderItemsToAdd.Any())
        {
            await _serviceOrderItems.AddRangeAsync(serviceOrderItemsToAdd, cancellationToken);
        }

        // Update ServiceOrder
        _serviceOrders.Update(serviceOrder);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> AreServicesAvailable(IEnumerable<Guid> serviceIds, CancellationToken cancellationToken)
    {
        var count = await _services
            .Where(service => serviceIds.Contains(service.Id) 
                    && service.RemovedAt == CommonConstant.MIN_DATE_TIME
                    && service.RemovedBy == CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            )
            .CountAsync();
       
        return count == serviceIds.Count();
    }

    public async Task<bool> IsServiceOrderExist(Guid serviceOrderId, CancellationToken CancellationToken)
    {
        return await _serviceOrders.AnyAsync(entity => entity.Id == serviceOrderId);
    }

}
