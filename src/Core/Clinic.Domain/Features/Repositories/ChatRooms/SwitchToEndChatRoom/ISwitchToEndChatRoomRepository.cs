using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatRooms.SwitchToEndChatRoom;

/// <summary>
///     Interface ISwitchToEndChatRoomRepository.
/// </summary>
public interface ISwitchToEndChatRoomRepository
{
    Task<bool> UpdateChatRoomStatusToEndCommandAsync(
        Guid chatRoomId,
        CancellationToken cancellationToken = default
    );
}
