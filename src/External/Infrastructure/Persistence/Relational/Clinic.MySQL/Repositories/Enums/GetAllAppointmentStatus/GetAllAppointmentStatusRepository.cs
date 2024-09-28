
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Enums.GetAllAppointmentStatus;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.MySQL.Repositories.Enums.GetAllAppointmentStatus;

/// <summary>
///    Implement of IGetAllAppointmentStatus repository.
/// </summary>
internal class GetAllAppointmentStatusRepository : IGetAllAppointmentStatusRepository
{
    private readonly ClinicContext _context;
    private DbSet<AppointmentStatus> _statusDetails;

    public GetAllAppointmentStatusRepository(ClinicContext context)
    {
        _context = context;
        _statusDetails = _context.Set<AppointmentStatus>();
    }

    public async Task<IEnumerable<AppointmentStatus>> FindAllAppointmentStatusQueryAsync(CancellationToken cancellationToken)
    {
        return await _statusDetails
           .AsNoTracking()        
           .Select(statusDetail => new AppointmentStatus()
           {
               Id = statusDetail.Id,
               StatusName = statusDetail.StatusName,
               Constant = statusDetail.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}