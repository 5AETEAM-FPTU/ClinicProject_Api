using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.RemoveService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;


namespace Clinic.MySQL.Repositories.ExaminationServices.RemoveService;

public class RemoveServiceRepository : IRemoveServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public RemoveServiceRepository(ClinicContext context)
    {
        _context = context;
        _services = _context.Set<Service>();
    }

    public async Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken)
    {
        return await _services.AnyAsync(service => service.Id == serviceId, cancellationToken);
    }

    public async Task<bool> RemoveServiceByIdCommandAsync(Guid Id, CancellationToken cancellationToken)
    {
        var removeService = await _services.FirstOrDefaultAsync(service => service.Id == Id, cancellationToken);

        _services.Remove(removeService);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
