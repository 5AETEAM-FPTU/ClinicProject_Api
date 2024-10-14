using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.QueueRooms.GetQueueRoomByUserId;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.GetQueueRoomByUserId;

/// <summary>
///     Implementation of <see cref="IGetQueueRoomByUserIdRepository"/>
/// </summary>
internal class GetQueueRoomByUserIdRepository : IGetQueueRoomByUserIdRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<QueueRoom> _queueRooms;

    public GetQueueRoomByUserIdRepository(ClinicContext context)
    {
        _context = context;
        _queueRooms = context.Set<QueueRoom>();
    }

    public Task<QueueRoom> FindQueueRoomByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return _queueRooms
            .Where(predicate: entity => entity.IsSuported == false && entity.PatientId == userId)
            .Select(selector: entity => new QueueRoom
            {
                Id = entity.Id,
                Message = entity.Message,
                Title = entity.Title
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
