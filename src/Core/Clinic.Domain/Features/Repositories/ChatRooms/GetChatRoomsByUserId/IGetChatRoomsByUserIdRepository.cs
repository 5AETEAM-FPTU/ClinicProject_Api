using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatRooms.GetChatRoomsByUserId;

/// <summary>
/// Interface IGetChatRoomsByUserIdRepository.
/// </summary>
public interface IGetChatRoomsByUserIdRepository
{
    Task<IEnumerable<ChatRoom>> FindAllChatRoomsByUserIdQueryAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
