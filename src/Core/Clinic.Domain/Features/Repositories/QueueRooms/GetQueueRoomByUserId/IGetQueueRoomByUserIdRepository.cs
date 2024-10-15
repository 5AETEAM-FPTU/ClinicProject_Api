using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.QueueRooms.GetQueueRoomByUserId;

/// <summary>
///     Interface IGetQueueRoomByUserIdRepository
/// </summary>
public interface IGetQueueRoomByUserIdRepository
{
    Task<QueueRoom> FindQueueRoomByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
