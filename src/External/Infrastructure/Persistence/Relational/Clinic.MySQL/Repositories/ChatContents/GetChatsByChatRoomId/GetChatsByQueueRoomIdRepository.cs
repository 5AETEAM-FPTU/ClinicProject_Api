using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatContents.GetChatsByChatRoomId;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     GetChatsByChatRoomId Repository
/// </summary>
internal class GetChatsByChatRoomIdRepository : IGetChatsByChatRoomIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatContent> _chatContents;
    private DbSet<ChatRoom> _chatRooms;

    public GetChatsByChatRoomIdRepository(ClinicContext context)
    {
        _context = context;
        _chatContents = _context.Set<ChatContent>();
        _chatRooms = _context.Set<ChatRoom>();
    }

    public async Task<IEnumerable<ChatContent>> FindChatContentsByChatRoomIdQueryAsync(
        Guid chatRoomId,
        DateTime lastTimeOfBefore,
        int limit,
        CancellationToken cancellationToken = default
    )
    {
        return await _chatContents
            .AsNoTracking()
            .Where(predicate: entity =>
                entity.ChatRoomId == chatRoomId && entity.CreatedAt <= lastTimeOfBefore
            )
            .OrderByDescending(entity => entity.CreatedAt)
            .Select(selector: entity => new ChatContent()
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                RemovedBy = entity.RemovedBy,
                Assets = entity
                    .Assets.Select(asset => new Asset()
                    {
                        FileName = asset.FileName,
                        FilePath = asset.FilePath,
                    })
                    .ToList(),
                IsRead = entity.IsRead,
                TextContent = entity.TextContent,
                SenderId = entity.SenderId,
            })
            .Take(count: limit)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsUserOwnerOfChatRoomQueryAsync(
        Guid queueRoomId,
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return _chatRooms.AnyAsync(
            predicate: entity =>
                (entity.DoctorId == userId || entity.PatientId == userId)
                && entity.Id == queueRoomId,
            cancellationToken: cancellationToken
        );
    }
}
