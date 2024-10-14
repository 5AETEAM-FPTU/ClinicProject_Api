using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.UpdateService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ExaminationServices.UpdateService;

public class UpdateServiceRepository : IUpdateServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public UpdateServiceRepository(ClinicContext context)
    {
        _context = context;
        _services = _context.Set<Service>();    
    }

    public Task<Service> GetServiceByIdCommandAsync(Guid id, CancellationToken cancellationToken)
    {
        return _services.FirstOrDefaultAsync(service => service.Id == id, cancellationToken);
    }

    public Task<bool> IsServiceExist(Guid id, CancellationToken cancellationToken)
    {
        return _services.AnyAsync(service => service.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateServiceCommandAsync(Service service, CancellationToken cancellationToken)
    {
        _services.Update(service);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> IsExistServiceCode(string code, CancellationToken cancellationToken)
    {
        return await _services.AnyAsync(entity => entity.Code.Equals(code), cancellationToken: cancellationToken);
    }
}
