using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatContents.CreateChatContent;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatContents.CreateChatContent;

/// <summary>
///     CreateChatContent Repository
/// </summary>
internal class CreateChatContentRepository : ICreateChatContentRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatContent> _chatContents;
    private DbSet<ChatRoom> _chatRooms;

    public CreateChatContentRepository(ClinicContext context)
    {
        _context = context;
        _chatContents = _context.Set<ChatContent>();
        _chatRooms = _context.Set<ChatRoom>();
    }

    public async Task<bool> AddChatContentCommandAsync(
        ChatContent chatContent,
        DateTime createdTime,
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    )
    {
        var dbResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    _chatContents.Add(entity: chatContent);

                    await _chatRooms
                        .Where(predicate: chatRoom => chatRoom.Id == chatRoomId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder.SetProperty(chatRoom => chatRoom.LatestTimeMessage, createdTime)
                        );

                    await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbResult = true;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                    dbResult = false;
                }
            });
        return dbResult;
    }

    public Task<bool> IsChatRoomExperiedQueryAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    )
    {
        return _chatRooms
            .Where(predicate: chatRoom => chatRoom.Id == chatRoomId && chatRoom.IsEnd)
            .AnyAsync(cancellationToken: cancellationToken);
    }
}
