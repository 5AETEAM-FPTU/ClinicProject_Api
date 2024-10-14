using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ExaminationServices.HiddenService;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.ExaminationServices.HiddenService;

public class HiddenServiceRepository : IHiddenServiceRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<Service> _services;

    public HiddenServiceRepository(ClinicContext context)
    {
        _context = context;
        _services = _context.Set<Service>();
    }

    public async Task<Service> GetServiceByIdQueryAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _services.FirstOrDefaultAsync(service => service.Id == Id, cancellationToken);
    }

    public async Task<bool> IsServiceExisted(Guid serviceId, CancellationToken cancellationToken)
    {
        return await _services.AnyAsync(service => service.Id == serviceId, cancellationToken);
    }

    public async Task<bool> RemoveServiceTemporarityCommandAsync(Service service, CancellationToken cancellationToken)
    {
        _services.Update(service);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

}
