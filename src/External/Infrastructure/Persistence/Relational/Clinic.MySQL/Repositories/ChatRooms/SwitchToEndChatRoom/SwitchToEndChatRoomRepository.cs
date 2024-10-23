using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatRooms.SwitchToEndChatRoom;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     Implementation of ISwitchToEndChatRoomRepository.
/// </summary>
internal class SwitchToEndChatRoomRepository : ISwitchToEndChatRoomRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatRoom> _chatRooms;

    public SwitchToEndChatRoomRepository(ClinicContext context)
    {
        _context = context;
        _chatRooms = _context.Set<ChatRoom>();
    }

    public async Task<bool> UpdateChatRoomStatusToEndCommandAsync(
        Guid ChatRoomId,
        CancellationToken cancellationToken = default
    )
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );
                try
                {
                    await _chatRooms
                        .Where(predicate: entity => entity.Id == ChatRoomId)
                        .ExecuteUpdateAsync(builder =>
                            builder.SetProperty(entity => entity.IsEnd, true)
                        );
                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransactionResult = true;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}
