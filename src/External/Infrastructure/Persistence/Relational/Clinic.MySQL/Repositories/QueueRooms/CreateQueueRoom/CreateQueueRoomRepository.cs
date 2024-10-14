using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.QueueRooms.CreateQueueRoom;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.OnlinePayments.CreateQueueRoom;

/// <summary>
///     Implementation of <see cref="ICreateQueueRoomRepository"/>
/// </summary>
internal class CreateQueueRoomRepository : ICreateQueueRoomRepository
{
    private readonly ClinicContext _context;
    private readonly DbSet<QueueRoom> _queueRooms;

    public CreateQueueRoomRepository(ClinicContext context)
    {
        _context = context;
        _queueRooms = context.Set<QueueRoom>();
    }

    public async Task<bool> AddQueueRoomCommandAsync(
        QueueRoom queueRoom,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _queueRooms.Add(queueRoom);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<bool> IsPatientRequestConsultantQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return _queueRooms
            .Where(predicate: queueRoom =>
                queueRoom.PatientId == userId && queueRoom.IsSuported == true
            )
            .AnyAsync(cancellationToken: cancellationToken);
    }
}
