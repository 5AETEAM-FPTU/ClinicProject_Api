using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatRooms.AssignChatRoom;

public interface IAssignChatRoomRepository
{
    Task<bool> AddChatRoomCommandAsync(
        ChatRoom chatRoom,
        CancellationToken cancellationToken = default
    );

    Task<bool> IsPatientFoundByIdQueryAsync(
        Guid patientId,
        CancellationToken cancellationToken = default
    );
}
