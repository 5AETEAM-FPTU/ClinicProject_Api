using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.QueueRooms.RemoveQueueRoom;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.RemoveQueueRoom;

/// <summary>
///     Implementation of <see cref="IRemoveQueueRoomRepository"/>
/// </summary>
internal class RemoveQueueRoomRepository : IRemoveQueueRoomRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<QueueRoom> _queueRooms;

    public RemoveQueueRoomRepository(ClinicContext context)
    {
        _context = context;
        _queueRooms = context.Set<QueueRoom>();
    }

    public async Task<bool> DeleteQueueRoomCommandAsync(
        Guid queueRoomId,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await _queueRooms
                .Where(entity => entity.Id == queueRoomId)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> IsQueueRoomOwnByUserAsync(
        Guid userId,
        Guid queueRoomId,
        CancellationToken cancellationToken = default
    )
    {
        return _queueRooms.AnyAsync(
            predicate: entity => entity.Id == queueRoomId && entity.PatientId == userId,
            cancellationToken: cancellationToken
        );
    }
}
