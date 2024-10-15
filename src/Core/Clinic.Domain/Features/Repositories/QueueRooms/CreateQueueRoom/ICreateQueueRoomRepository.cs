using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.QueueRooms.CreateQueueRoom;

/// <summary>
///     Interface ICreateQueueRoomRepository
/// </summary>
public interface ICreateQueueRoomRepository
{
    Task<bool> AddQueueRoomCommandAsync(
        QueueRoom queueRoom,
        CancellationToken cancellationToken = default
    );

    Task<bool> IsPatientRequestConsultantQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
