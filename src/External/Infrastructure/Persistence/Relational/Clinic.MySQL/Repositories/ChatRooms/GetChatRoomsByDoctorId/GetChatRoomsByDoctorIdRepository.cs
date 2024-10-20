using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByDoctorId;
using Clinic.MySQL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.GetChatRoomsByDoctorId;

/// <summary>
///     Implementation of IGetChatRoomsByDoctorIdRepository.
/// </summary>
internal class GetChatRoomsByDoctorIdRepository : IGetChatRoomsByDoctorIdRepository
{
    private readonly ClinicContext _context;
    private DbSet<ChatRoom> _chatRooms;

    public GetChatRoomsByDoctorIdRepository(ClinicContext context)
    {
        _context = context;
        _chatRooms = _context.Set<ChatRoom>();
    }

    public async Task<IEnumerable<ChatRoom>> FindAllChatRoomsByDoctorIdQueryAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default
    )
    {
        return await _chatRooms
            .AsNoTracking()
            .Where(predicate: chatRoom => chatRoom.DoctorId == doctorId)
            .OrderByDescending(keySelector: chatRoom => chatRoom.LatestTimeMessage)
            .Select(selector: chatRoom => new ChatRoom()
            {
                Id = chatRoom.Id,
                IsEnd = chatRoom.IsEnd,
                LastMessage = chatRoom.LastMessage,
                ExpiredTime = chatRoom.ExpiredTime,
                LatestTimeMessage = chatRoom.LatestTimeMessage,
                Patient = new()
                {
                    User = new()
                    {
                        Id = chatRoom.Patient.User.Id,
                        Avatar = chatRoom.Patient.User.Avatar,
                        FullName = chatRoom.Patient.User.FullName
                    }
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
