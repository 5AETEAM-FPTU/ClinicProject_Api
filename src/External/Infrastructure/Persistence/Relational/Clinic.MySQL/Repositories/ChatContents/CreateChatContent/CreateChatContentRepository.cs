using System;
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
    }

    public async Task<bool> AddChatContentCommandAsync(
        ChatContent chatContent,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _chatContents.Add(entity: chatContent);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            return false;
        }
    }

    public Task<bool> IsChatRoomExperiedQueryAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    )
    {
        return _chatRooms
            .Where(predicate: chatRoom =>
                chatRoom.Id == chatRoomId && chatRoom.ExpiredTime < DateTime.Now
            )
            .AnyAsync(cancellationToken: cancellationToken);
    }
}
