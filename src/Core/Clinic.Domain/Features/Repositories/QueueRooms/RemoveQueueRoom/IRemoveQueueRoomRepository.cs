using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.QueueRooms.RemoveQueueRoom;

/// <summary>
///     Interface IRemoveQueueRoomRepository
/// </summary>
public interface IRemoveQueueRoomRepository
{
    Task<bool> DeleteQueueRoomCommandAsync(
        Guid queueRoomId,
        CancellationToken cancellationToken = default
    );

    Task<bool> IsQueueRoomOwnByUserAsync(
        Guid userId,
        Guid queueRoomId,
        CancellationToken cancellationToken = default
    );
}
