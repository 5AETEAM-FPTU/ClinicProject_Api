using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatRooms.CancelConversationChatRoom;

/// <summary>
/// Interface ICancelConversationChatRoomRepository.
/// </summary>
public interface ICancelConversationChatRoomRepository
{
    Task<bool> SwitchChatRoomStatusToEndCommandAsync(CancellationToken cancellationToken = default);
}
