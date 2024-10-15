using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;

namespace Clinic.Domain.Features.Repositories.ChatContents.GetChatsByChatRoomId;

/// <summary>
///     Interface for GetChatsByRoomChatId Repository.
/// </summary>
public interface IGetChatsByChatRoomIdRepository
{
    Task<bool> IsUserOwnerOfChatRoomQueryAsync(
        Guid chatRoomId,
        Guid userId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ChatContent>> FindChatContentsByChatRoomIdQueryAsync(
        Guid chatRoomId,
        DateTime lastTimeOfBefore,
        int limit,
        CancellationToken cancellationToken = default
    );
}
