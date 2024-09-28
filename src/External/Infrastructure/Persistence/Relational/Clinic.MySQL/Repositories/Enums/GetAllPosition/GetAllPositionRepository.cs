using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Clinic.Domain.Features.Repositories.Enums.GetAllPosition;
using Clinic.Domain.Commons.Entities;
using System.Linq;

namespace Clinic.MySQL.Repositories.Enums.GetAllPosition;

/// <summary>
///    Implement of IGetAllPosition repository.
/// </summary>
internal class GetAllPositionRepository : IGetAllPositionRepository
{
    private readonly ClinicContext _context;
    private DbSet<Position> _positionDetails;

    public GetAllPositionRepository(ClinicContext context)
    {
        _context = context;
        _positionDetails = _context.Set<Position>();
    }

    public async Task<IEnumerable<Position>> FindAllPositionQueryAsync(CancellationToken cancellationToken)
    {
        return await _positionDetails
           .AsNoTracking()
           .Select(positionDetail => new Position()
           {
               Id = positionDetail.Id,
               Name = positionDetail.Name,
               Constant = positionDetail.Constant,
           })
           .ToListAsync(cancellationToken: cancellationToken);
    }
}
