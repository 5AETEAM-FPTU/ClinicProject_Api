using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.QueueRooms.GetAllQueueRooms;

/// <summary>
///     Interface IGetAllQueueRoomsRepository
/// </summary>
public interface IGetAllQueueRoomsRepository
{
    Task<IEnumerable<QueueRoom>> FindAllQueueRoomsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default
    );
}
