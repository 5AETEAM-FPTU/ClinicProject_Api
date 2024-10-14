using System;
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

    public AssignChatRoomRepository(ClinicContext context)
    {
        _context = context;
        _chatRooms = _context.Set<ChatRoom>();
        _patients = _context.Set<Patient>();
    }

    public async Task<bool> AddChatRoomCommandAsync(
        ChatRoom chatRoom,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            _chatRooms.Add(chatRoom);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<bool> IsPatientFoundByIdQueryAsync(
        Guid patientId,
        CancellationToken cancellationToken = default
    )
    {
        return _patients.AnyAsync(entity => entity.UserId.Equals(patientId));
    }
}
