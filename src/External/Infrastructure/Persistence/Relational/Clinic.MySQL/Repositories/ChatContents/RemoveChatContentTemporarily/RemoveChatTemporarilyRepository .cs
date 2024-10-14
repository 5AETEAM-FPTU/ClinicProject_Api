using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatContents.RemoveChatContentTemporarily;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.RemoveChatContentTemporarily;

/// <summary>
///     RemoveChatContentTemporarily Repository
/// </summary>
internal class RemoveChatTemporarilyRepository : IRemoveChatContentTemporarilyRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatContent> _chatContents;

    public RemoveChatTemporarilyRepository(ClinicContext context)
    {
        _context = context;
        _chatContents = _context.Set<ChatContent>();
    }

    public async Task<bool> DeleteChatContentByIdCommandAsync(
        Guid chatContentId,
        Guid removedBy,
        DateTime removedAt,
        CancellationToken cancellationToken = default
    )
    {
        var dbTransaction = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );
                try
                {
                    await _chatContents
                        .Where(entity => entity.Id == chatContentId)
                        .ExecuteUpdateAsync(setPropertyCalls: builder =>
                            builder
                                .SetProperty(entity => entity.RemovedAt, removedAt)
                                .SetProperty(entity => entity.RemovedBy, removedBy)
                        );

                    await transaction.CommitAsync(cancellationToken: cancellationToken);

                    dbTransaction = true;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });
        return dbTransaction;
    }

    public Task<bool> IsChatContentExistByIdQueryAsync(
        Guid chatContentId,
        CancellationToken cancellationToken = default
    )
    {
        return _chatContents.AnyAsync(
            entity => entity.Id == chatContentId,
            cancellationToken: cancellationToken
        );
    }
}
