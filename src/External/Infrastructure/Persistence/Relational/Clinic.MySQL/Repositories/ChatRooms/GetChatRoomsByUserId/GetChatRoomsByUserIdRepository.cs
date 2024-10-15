using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByUserId;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.GetChatRoomsByUserId;

/// <summary>
///     Implementation of IGetChatRoomsByUserIdRepository.
/// </summary>
internal class GetChatRoomsByUserIdRepository : IGetChatRoomsByUserIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatRoom> _chatRooms;

    public GetChatRoomsByUserIdRepository(ClinicContext context)
    {
        _context = context;
        _chatRooms = _context.Set<ChatRoom>();
    }

    public async Task<IEnumerable<ChatRoom>> FindAllChatRoomsByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    )
    {
        return await _chatRooms
            .AsNoTracking()
            .Where(predicate: chatRoom => chatRoom.PatientId == userId)
            .Select(selector: chatRoom => new ChatRoom()
            {
                Id = chatRoom.Id,
                IsEnd = chatRoom.IsEnd,
                LastMessage = chatRoom.LastMessage,
                Doctor = new()
                {
                    User = new()
                    {
                        Id = chatRoom.Doctor.User.Id,
                        Avatar = chatRoom.Doctor.User.Avatar,
                        FullName = chatRoom.Doctor.User.FullName
                    }
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
