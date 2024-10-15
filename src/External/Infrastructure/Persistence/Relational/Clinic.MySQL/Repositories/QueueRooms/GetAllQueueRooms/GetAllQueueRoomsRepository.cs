using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.QueueRooms.GetAllQueueRooms;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.GetAllQueueRooms;

/// <summary>
///     Implementation of <see cref="IGetAllQueueRoomsRepository"/>
/// </summary>
internal class GetAllQueueRoomsRepository : IGetAllQueueRoomsRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<QueueRoom> _queueRooms;

    public GetAllQueueRoomsRepository(ClinicContext context)
    {
        _context = context;
        _queueRooms = context.Set<QueueRoom>();
    }

    public async Task<IEnumerable<QueueRoom>> FindAllQueueRoomsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        return await _queueRooms
            .AsNoTracking()
            .Where(predicate: entity => entity.IsSuported)
            .OrderBy(keySelector: entity => entity.CreatedAt)
            .Select(selector: entity => new QueueRoom()
            {
                Id = entity.Id,
                Title = entity.Title,
                Message = entity.Message,
                Patient = new()
                {
                    User = new()
                    {
                        FullName = entity.Patient.User.FullName,
                        Avatar = entity.Patient.User.Avatar,
                    }
                }
            })
            .Take(count: pageSize)
            .Skip(count: (pageIndex - 1) * pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
