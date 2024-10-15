using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatRooms.AssignChatRoom;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.AssignChatRoom;

internal class AssignChatRoomRepository : IAssignChatRoomRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatRoom> _chatRooms;
    private DbSet<Patient> _patients;
    private DbSet<QueueRoom> _queueRooms;

    public AssignChatRoomRepository(ClinicContext context)
    {
        _context = context;
        _chatRooms = _context.Set<ChatRoom>();
        _patients = _context.Set<Patient>();
        _queueRooms = _context.Set<QueueRoom>();
    }

    public async Task<bool> AddChatRoomCommandAsync(
        Guid queueRoomId,
        ChatRoom chatRoom,
        CancellationToken cancellationToken = default
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );

                try
                {
                    _chatRooms.Add(entity: chatRoom);
                    await _queueRooms
                        .Where(predicate: entity => entity.Id == queueRoomId)
                        .ExecuteUpdateAsync(setPropertyCalls: entity =>
                            entity.SetProperty(x => x.IsSuported, true)
                        );

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                    dbTransactionResult = true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                    dbTransactionResult = false;
                }
            });
        return dbTransactionResult;
    }

    public Task<bool> IsPatientFoundByIdQueryAsync(
        Guid patientId,
        CancellationToken cancellationToken = default
    )
    {
        return _patients.AnyAsync(entity => entity.UserId.Equals(patientId));
    }
}
